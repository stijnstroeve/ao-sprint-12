const fs = require('fs');
const sql = require('mssql/msnodesqlv8');
const inputFile = 'Generated/output.json';
const {createArtist, createSongArtist, createSongGenre, createSongRank, createSong, createGenre} = require('./sql');

const songRanks = [];
const songGenres = [];
const songArtists = [];

// const createdGenreNames = [];

const start = async () => {
    try {
        const conn = {
            driver: "msnodesqlv8",
            connectionString: "Driver={SQL Server Native Client 11.0};Server=localhost;Database=Top2000;Trusted_Connection={yes};"
        };
        await sql.connect(conn)

        fs.readFile(inputFile, 'utf8', async (err, fileString) => {
            if(!err) {
                const data = JSON.parse(fileString);

                for(const song of data) {
                    // Add the song
                    const id = await createSong(song);

                    song.rank_entries.forEach(entry => {
                        songRanks.push({
                            year: entry.year,
                            rank: entry.rank,
                            song_id: id
                        });
                    });

                    // Add the genres
                    if(song.genres) {
                        for(const genre of song.genres) {
                            const foundGenre = songGenres.find((fGenre) => fGenre.name === genre);
                            if(!foundGenre) {
                                const genreId = await createGenre(genre);

                                if(genreId) {
                                    songGenres.push({
                                        name: genre,
                                        genre_id: genreId,
                                        song_id: id
                                    });
                                }
                            } else {
                                songGenres.push({
                                    name: genre,
                                    genre_id: foundGenre.genre_id,
                                    song_id: id
                                });
                            }
                        }
                    }

                    // Add the artists
                    if(song.artists) {
                        for(const artist of song.artists) {
                            const foundArtist = songArtists.find((fArtist) => fArtist.name === artist);
                            if(!foundArtist) {
                                const artistId = await createArtist(artist);

                                if(artistId) {
                                    songArtists.push({
                                        name: artist,
                                        artist_id: artistId,
                                        song_id: id
                                    });
                                }
                            } else {
                                songArtists.push({
                                    name: artist,
                                    artist_id: foundArtist.artist_id,
                                    song_id: id
                                });
                            }
                        }
                    }

                }


                //
                // // Add the song ranks
                for(const songRank of songRanks) {
                    await createSongRank(songRank);
                }
                //
                // Add the song genres
                for(const songGenre of songGenres) {
                    await createSongGenre(songGenre);
                }

                // Add the song artists
                for(const songArtist of songArtists) {
                    await createSongArtist(songArtist);
                }

            } else {
                console.error(err);
            }
        });

    } catch (err) {
        console.log(err);
    }
}
start();
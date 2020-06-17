const fs = require('fs');
const Spotify = require('node-spotify-api');
const stringSimilarity = require('string-similarity');
const _ = require('lodash');

const spotify = new Spotify({
    id: '5ec00383ae8b4c3ea7a4b63a3950cda3',
    secret: 'b9c34297a47c4da798387cf208c2e852' // No, this one does not work anymore :D
});

const searchSongByArtist = (songName, artistName, maxBias = 30) => {
    console.log(`Fetching ${songName} from spotify.`);
    return spotify
        .search({ market: 'NL', type: 'track', query: songName, limit: maxBias })
        .then(function(data) {
            // Make sure the artist is right
            return data.tracks.items.find(track => stringSimilarity.compareTwoStrings(track.artists[0].name.toUpperCase(), artistName.toUpperCase()) > 0.2);
        })
        .catch(function(err) {
            console.error('Error occurred: ' + err);
            return {};
        });
}

const searchSongByArtistMemoized = _.memoize(searchSongByArtist);

const getGenresByArtistId = async (artistId) => {
    return spotify
        .request('https://api.spotify.com/v1/artists/' + artistId)
        .then(function(data) {
            // Make sure the artist is right
            return data.genres;
        })
        .catch(function(err) {
            console.error('Error occurred: ' + err);
            return [];
        });
}

const getGenresByArtistIdMemoized = _.memoize(getGenresByArtistId);

const getGeneralInfo = (track) => {
    const info = {};
    if(track && track.album) {
        if('release_date' in track.album) {
            const splitDate = track.album.release_date.split('-');
            info['release_date'] = {
                full_date: track.album.release_date,
                year: parseInt(splitDate[0]),
                month: parseInt(splitDate[1]),
                day: parseInt(splitDate[2]),
            };
        }
        if('images' in track.album) {
            info['images'] = track.album.images;
        }
    }
    if(track && track.preview_url) {
        info['preview_url'] = track.preview_url;
    }
    return info;
}

const toTitleCase = (phrase) => {
    return phrase
        .toLowerCase()
        .split(' ')
        .map(word => word.charAt(0).toUpperCase() + word.slice(1))
        .join(' ');
};

const getGenres = async (fetchedSong) => {
    if(fetchedSong && fetchedSong.artists.length > 0) {
        const fetchedGenres = await getGenresByArtistIdMemoized(fetchedSong.artists[0].id);
        return fetchedGenres.map(genre => toTitleCase(genre));
    } else {
        return [];
    }
}

const getArtists = (fetchedSong) => {
    if(fetchedSong && fetchedSong.artists.length > 0) {
        return fetchedSong.artists.map(artist => artist.name)
    }
    return [];
}

const songs = [];

const findSong = (songName, artistName) => {
    return songs.find(song => song.title === songName && song.full_artist === artistName)
}

const folderToRead = 'Data';

fs.readdir(folderToRead, async (err, files) => {
    //handling error
    if (err) {
        return console.log('Unable to scan directory: ' + err);
    }
    //listing all files using forEach
    for(const fileName of files) {
        // Do whatever you want to do with the file
        console.log(`Reading ${fileName}...`);

        const file = fs.readFileSync(folderToRead + '/' + fileName, 'utf8');

        const splitFile = file.split('\n');
        console.log(splitFile[0]);

        const currentYear = parseInt(splitFile[0].split(';')[0]);

        for (let i = 1; i < splitFile.length - 1; i++) {
            const line = splitFile[i];
            const splitSong = line.split(';');


            const foundSong = findSong(splitSong[1], splitSong[2]);

            if(!foundSong) {
                // Song is not registered yet
                const song = {
                    title: splitSong[1],
                    full_artist: splitSong[2],
                    rank_entries: [{year: currentYear, rank: parseInt(splitSong[0])}],
                };

                const fetchedSong = await searchSongByArtistMemoized(song.title, splitSong[2]);
                song['genres'] = await getGenres(fetchedSong);

                song['artists'] = getArtists(fetchedSong);

                const generalInfo = getGeneralInfo(fetchedSong);
                Object.assign(song, generalInfo);

                songs.push(song);
            } else {
                console.log(`Song ${splitSong[1]} already cached > Adding new entry`);
                // Song is already found, update the entries
                foundSong.rank_entries.push({year: currentYear, rank: parseInt(splitSong[0])});
            }

            console.log(`Done ${i}/${splitFile.length - 2}`);
        }

        fs.writeFile(`Output/tmp-${fileName}.json`, JSON.stringify(songs, null, 4), (err) => {
            if(err) {
                console.log(err);
            } else {
                console.log('Generated tmp file successfully!');
            }
        })

    }

    fs.writeFile('Output/output.json', JSON.stringify(songs, null, 4), (err) => {
        if(err) {
            console.log(err);
        } else {
            console.log('Generated output.json successfully!');
        }
    })
});





// test();

// console.log(stringSimilarity.compareTwoStrings('test', 'vest'));
// console.log(stringSimilarity.compareTwoStrings('CHUNG HA', 'Danny Vera'));
// console.log(stringSimilarity.compareTwoStrings('DennyVera', 'Danny Vera'));


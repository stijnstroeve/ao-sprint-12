const sql = require('mssql/msnodesqlv8');

const createGenre = (genre) => {
    return new Promise((resolve, reject) => {
        const request = new sql.Request();
        request.
        input('GenreName', genre).
        query('INSERT INTO [dbo].[Genre] (GenreName) OUTPUT INSERTED.GenreID VALUES (@GenreName)',function(err, data){
            if(err){
                console.log("Error while connecting database :- " + err);
                reject(err);
            } else{
                console.log(data);
                resolve(data.recordset[0].GenreID);
            }
        });
    })
}

const createSongGenre = (songGenreData) => {
    return new Promise((resolve, reject) => {
        const request = new sql.Request();
        request.
        input('SongID', songGenreData.song_id).
        input('GenreID', songGenreData.genre_id).
        query('INSERT INTO [dbo].[SongGenre] (SongID, GenreID) VALUES (@SongID, @GenreID)',function(err, data){
            if(err){
                console.log("Error while connecting database :- " + err);
                reject(err);
            } else{
                console.log(data);
                resolve();
            }
        });
    })
}

const createArtist = (artist) => {
    return new Promise((resolve, reject) => {
        const request = new sql.Request();
        request.
        input('ArtistName', artist).
        query('INSERT INTO [dbo].[Artist] (ArtistName) OUTPUT INSERTED.ArtistID VALUES (@ArtistName)',function(err, data){
            if(err){
                console.log("Error while connecting database :- " + err);
                reject(err);
            } else{
                console.log(data);
                resolve(data.recordset[0].ArtistID);
            }
        });
    })
}

const createSongArtist = (songArtistData) => {
    return new Promise((resolve, reject) => {
        const request = new sql.Request();
        request.
        input('SongID', songArtistData.song_id).
        input('ArtistID', songArtistData.artist_id).
        query('INSERT INTO [dbo].[SongArtist] (SongID, ArtistID) VALUES (@SongID, @ArtistID)',function(err, data){
            if(err){
                console.log("Error while connecting database :- " + err);
                reject(err);
            } else{
                console.log(data);
                resolve();
            }
        });
    })
}


const createSong = (songData) => {
    return new Promise((resolve, reject) => {
        const request = new sql.Request();
        request.
        input('Title', songData.title).
        input('ReleaseDate', new Date(songData.release_date ?songData.release_date.full_date : Date.now())).
        input('ExternalImageUrl', songData.images && songData.images.length > 1 ? songData.images[1].url : 'NULL').
        input('ExternalSampleUrl', songData.preview_url || 'NULL').
        query('INSERT INTO [dbo].[Song] (SongTitle, ReleaseDate, ExternalImageUrl, ExternalSampleUrl) OUTPUT INSERTED.SongID VALUES (@Title, @ReleaseDate, @ExternalImageUrl, @ExternalSampleUrl)',function(err, data){
            if(err){
                console.log("Error while connecting database :- " + err);
                reject(err);
            } else{
                console.log(data);
                resolve(data.recordset[0].SongID);
            }
        });
    })
}

const createSongRank = (songRankData) => {
    return new Promise((resolve, reject) => {
        const request = new sql.Request();
        request.
        input('Rank', songRankData.rank).
        input('Year', songRankData.year).
        input('SongID', songRankData.song_id).
        query('INSERT INTO [dbo].[SongRank] (Rank, Year, SongID) VALUES (@Rank, @Year, @SongID)',function(err, data){
            if(err){
                console.log("Error while connecting database :- " + err);
                reject(err);
            }
            else{
                console.log(data);
                resolve();
            }
        });
    })
}

module.exports = {
    createGenre,
    createSongGenre,
    createSong,
    createSongRank,
    createArtist,
    createSongArtist
}
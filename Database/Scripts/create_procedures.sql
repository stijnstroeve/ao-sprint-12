-- Select all songs
CREATE PROCEDURE spSelectAllTitles
AS   
    SELECT
		SongID,
		SongTitle,
		ReleaseDate
	FROM Song
GO  

-- Select number of songs per artist
CREATE PROCEDURE spNumberOfSongsOfArtist
AS
	SELECT
		Artist.ArtistName,
		COUNT(Song.SongID) AS 'SongCount'
	FROM
		Song 
	INNER JOIN SongArtist ON 
		Song.SongID = SongArtist.SongID 
	INNER JOIN Artist ON
		SongArtist.ArtistID = Artist.ArtistID
	GROUP BY
		Artist.ArtistName
GO

-- Search artist by name
CREATE PROCEDURE spSelectSearchedArtist
	@ArtistName nvarchar(255)
AS
	SELECT * FROM Artist WHERE ArtistName = @ArtistName
GO

-- Select top 10 songs by year
CREATE PROCEDURE spSelectTop10ListingOnYear
	@Year integer
AS
	SELECT TOP 10
		* 
	FROM
		Song
	INNER JOIN SongRank ON 
		Song.SongID = SongRank.SongID
	ORDER BY
		SongRank.Rank ASC
GO

-- Select all artists
CREATE PROCEDURE spSelectAllArtists
AS   
    SELECT * FROM Artist
GO  

-- Create a song
CREATE PROCEDURE spInsertNewSong
	@Title nvarchar(255),
	@ReleaseDate Date  
AS
	INSERT INTO Song (SongTitle, ReleaseDate) VALUES (@Title, @ReleaseDate)
GO

-- Create a rank entry
CREATE PROCEDURE spInsertNewRankEntry
	@SongID integer,
	@Year integer,
	@Rank integer 
AS
	INSERT INTO SongRank (SongID, Year, Rank) VALUES (@SongID, @Year, @Rank)
GO

-- Select all songs from a year
CREATE PROCEDURE spSelectListingOnYear
    @Year integer  
AS
	SELECT 
		Song.SongID,
		Song.SongTitle,
		Song.ReleaseDate,
		SongRank.Rank
	FROM 
		Song 
	INNER JOIN SongRank 
		ON Song.SongID = SongRank.SongID 
	WHERE 
		SongRank.Year = @Year
GO
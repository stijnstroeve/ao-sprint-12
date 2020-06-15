-- Select all songs
CREATE PROCEDURE spSelectAllTitles
AS   
    SELECT
		SongID,
		SongTitle,
		ReleaseDate
	FROM Song
GO  
 
EXEC spSelectAllTitles
GO

-- Select number of songs per artist
CREATE PROCEDURE spNumberOfSongsOfArtist
AS
	SELECT
		Artist.ArtistName as 'Artiest',
		COUNT(Song.SongID) AS 'Aantal nummers'
	FROM
		Song 
	INNER JOIN SongArtist ON 
		Song.SongID = SongArtist.SongID 
	INNER JOIN Artist ON
		SongArtist.ArtistID = Artist.ArtistID
	GROUP BY
		Artist.ArtistName
GO
EXEC spNumberOfSongsOfArtist;
GO

-- Search artist by name
CREATE PROCEDURE spSelectSearchedArtist
	@ArtistName nvarchar(255)
AS
	SELECT * FROM Artist WHERE ArtistName = @ArtistName
GO

EXEC spSelectSearchedArtist @ArtistName = 'Django Wagner';
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

EXEC spSelectTop10ListingOnYear @Year = 2018;
GO

-- Select all artists
CREATE PROCEDURE spSelectAllArtists
AS   
    SELECT * FROM Artist
GO  

EXEC spSelectAllArtists;
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
	INSERT INTO Rank (Year, Rank) VALUES (@Year, @Rank)
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

EXEC spSelectListingOnYear @Year = 2018
GO


/* Debugging */



EXEC spSelectAllTitles;
GO
DECLARE @tmp DATETIME
SET @tmp = GETDATE()
/* EXEC spInsertNewSong @Title = 'Kali', @ReleaseDate = @tmp */
/*EXEC spInsertNewRankEntry @SongID = 1, @Year = 2018, @Rank = 1431*/
GO
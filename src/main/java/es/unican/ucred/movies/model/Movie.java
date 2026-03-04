package es.unican.ucred.movies.model;

/**
 * Represents a movie from TMDb API.
 */
public class Movie {
    private final int id;
    private final String title;
    private final String overview;
    private final String posterPath;
    private final String releaseDate;
    private final double voteAverage;
    private final int voteCount;

    public Movie(int id, String title, String overview, String posterPath, 
                 String releaseDate, double voteAverage, int voteCount) {
        this.id = id;
        this.title = title;
        this.overview = overview;
        this.posterPath = posterPath;
        this.releaseDate = releaseDate;
        this.voteAverage = voteAverage;
        this.voteCount = voteCount;
    }

    public int getId() {
        return id;
    }

    public String getTitle() {
        return title;
    }

    public String getOverview() {
        return overview;
    }

    public String getPosterPath() {
        return posterPath;
    }

    public String getPosterUrl() {
        if (posterPath == null || posterPath.isEmpty()) {
            return null;
        }
        return "https://image.tmdb.org/t/p/w300" + posterPath;
    }

    public String getLargePosterUrl() {
        if (posterPath == null || posterPath.isEmpty()) {
            return null;
        }
        return "https://image.tmdb.org/t/p/w500" + posterPath;
    }

    public String getReleaseDate() {
        return releaseDate;
    }

    public double getVoteAverage() {
        return voteAverage;
    }

    public int getVoteCount() {
        return voteCount;
    }

    public String getYear() {
        if (releaseDate == null || releaseDate.length() < 4) {
            return "N/A";
        }
        return releaseDate.substring(0, 4);
    }

    public String getRatingString() {
        return String.format("%.1f/10", voteAverage);
    }

    @Override
    public String toString() {
        return title + " (" + getYear() + ")";
    }
}

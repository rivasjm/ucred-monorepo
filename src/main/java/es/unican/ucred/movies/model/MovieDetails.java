package es.unican.ucred.movies.model;

import java.util.List;

/**
 * Represents detailed information about a movie from TMDb API.
 */
public class MovieDetails extends Movie {
    private final List<String> genres;
    private final int runtime;
    private final String tagline;
    private final long budget;
    private final long revenue;

    public MovieDetails(int id, String title, String overview, String posterPath,
                       String releaseDate, double voteAverage, int voteCount,
                       List<String> genres, int runtime, String tagline,
                       long budget, long revenue) {
        super(id, title, overview, posterPath, releaseDate, voteAverage, voteCount);
        this.genres = genres;
        this.runtime = runtime;
        this.tagline = tagline;
        this.budget = budget;
        this.revenue = revenue;
    }

    public List<String> getGenres() {
        return genres;
    }

    public String getGenresString() {
        if (genres == null || genres.isEmpty()) {
            return "N/A";
        }
        return String.join(", ", genres);
    }

    public int getRuntime() {
        return runtime;
    }

    public String getRuntimeString() {
        if (runtime == 0) {
            return "N/A";
        }
        int hours = runtime / 60;
        int minutes = runtime % 60;
        return hours + "h " + minutes + "m";
    }

    public String getTagline() {
        return tagline != null && !tagline.isEmpty() ? tagline : "";
    }

    public long getBudget() {
        return budget;
    }

    public long getRevenue() {
        return revenue;
    }

    public String getBudgetString() {
        if (budget == 0) return "N/A";
        return "$" + String.format("%,d", budget);
    }

    public String getRevenueString() {
        if (revenue == 0) return "N/A";
        return "$" + String.format("%,d", revenue);
    }
}

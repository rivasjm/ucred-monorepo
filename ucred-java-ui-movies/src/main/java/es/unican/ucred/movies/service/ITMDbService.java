package es.unican.ucred.movies.service;

import es.unican.ucred.movies.model.Movie;
import es.unican.ucred.movies.model.MovieDetails;

import java.io.IOException;
import java.util.List;

/**
 * Interface for TMDb API service.
 */
public interface ITMDbService {
    /**
     * Get list of popular movies.
     * @return List of popular movies
     * @throws IOException if API call fails
     */
    List<Movie> getPopularMovies() throws IOException;

    /**
     * Get detailed information about a specific movie.
     * @param movieId The TMDb movie ID
     * @return Detailed movie information
     * @throws IOException if API call fails
     */
    MovieDetails getMovieDetails(int movieId) throws IOException;
}

package es.unican.ucred.movies.service;

import es.unican.ucred.movies.model.Movie;
import es.unican.ucred.movies.model.MovieDetails;
import okhttp3.mockwebserver.MockWebServer;
import okhttp3.mockwebserver.MockResponse;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.List;

import static org.junit.jupiter.api.Assertions.*;

class TMDbServiceTest {
    private MockWebServer mockServer;
    private TMDbService service;

    @BeforeEach
    void setUp() throws IOException {
        mockServer = new MockWebServer();
        mockServer.start();
        String baseUrl = mockServer.url("/").toString().replaceAll("/$", "");
        service = new TMDbService("test-api-key", baseUrl);
    }

    @AfterEach
    void tearDown() throws IOException {
        mockServer.shutdown();
    }

    @Test
    void getPopularMovies_Success() throws IOException {
        String jsonResponse = loadTestResource("popular_movies.json");
        mockServer.enqueue(new MockResponse()
                .setBody(jsonResponse)
                .setResponseCode(200)
                .addHeader("Content-Type", "application/json"));

        List<Movie> movies = service.getPopularMovies();

        assertNotNull(movies);
        assertEquals(3, movies.size());
        
        Movie firstMovie = movies.get(0);
        assertEquals(550, firstMovie.getId());
        assertEquals("El club de la lucha", firstMovie.getTitle());
        assertEquals("1999", firstMovie.getYear());
        assertEquals("8.4/10", firstMovie.getRatingString());
        assertTrue(firstMovie.getPosterUrl().contains("w300"));
    }

    @Test
    void getPopularMovies_ApiError() {
        mockServer.enqueue(new MockResponse().setResponseCode(401));

        IOException exception = assertThrows(IOException.class, () -> {
            service.getPopularMovies();
        });

        assertTrue(exception.getMessage().contains("401"));
    }

    @Test
    void getMovieDetails_ValidId() throws IOException {
        String jsonResponse = loadTestResource("movie_details.json");
        mockServer.enqueue(new MockResponse()
                .setBody(jsonResponse)
                .setResponseCode(200)
                .addHeader("Content-Type", "application/json"));

        MovieDetails details = service.getMovieDetails(550);

        assertNotNull(details);
        assertEquals(550, details.getId());
        assertEquals("El club de la lucha", details.getTitle());
        assertEquals("Drama, Suspense", details.getGenresString());
        assertEquals("2h 19m", details.getRuntimeString());
        assertEquals("$63,000,000", details.getBudgetString());
        assertEquals("$100,853,753", details.getRevenueString());
        assertEquals("Romper las reglas no es el problema. El problema es la ausencia de reglas.", 
                     details.getTagline());
    }

    @Test
    void getMovieDetails_InvalidId() {
        mockServer.enqueue(new MockResponse().setResponseCode(404));

        IOException exception = assertThrows(IOException.class, () -> {
            service.getMovieDetails(999999);
        });

        assertTrue(exception.getMessage().contains("404"));
    }

    @Test
    void constructor_NullApiKey_ThrowsException() {
        assertThrows(IllegalArgumentException.class, () -> {
            new TMDbService(null);
        });
    }

    @Test
    void constructor_EmptyApiKey_ThrowsException() {
        assertThrows(IllegalArgumentException.class, () -> {
            new TMDbService("");
        });
    }

    private String loadTestResource(String filename) throws IOException {
        String path = "src/test/resources/" + filename;
        return new String(Files.readAllBytes(Paths.get(path)));
    }
}

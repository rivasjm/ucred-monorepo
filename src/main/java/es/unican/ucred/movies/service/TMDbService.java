package es.unican.ucred.movies.service;

import com.google.gson.*;
import es.unican.ucred.movies.model.Movie;
import es.unican.ucred.movies.model.MovieDetails;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

/**
 * Implementation of TMDb API service using OkHttp and Gson.
 */
public class TMDbService implements ITMDbService {
    private static final String DEFAULT_BASE_URL = "https://api.themoviedb.org/3";
    
    private final String apiKey;
    private final String baseUrl;
    private final OkHttpClient client;
    private final Gson gson;

    public TMDbService(String apiKey) {
        this(apiKey, DEFAULT_BASE_URL);
    }

    public TMDbService(String apiKey, String baseUrl) {
        if (apiKey == null || apiKey.isEmpty()) {
            throw new IllegalArgumentException("API key cannot be null or empty");
        }
        this.apiKey = apiKey;
        this.baseUrl = baseUrl;
        this.client = new OkHttpClient();
        this.gson = new Gson();
    }

    @Override
    public List<Movie> getPopularMovies() throws IOException {
        String url = baseUrl + "/movie/popular?api_key=" + apiKey + "&language=es-ES";
        
        Request request = new Request.Builder()
                .url(url)
                .build();

        try (Response response = client.newCall(request).execute()) {
            if (!response.isSuccessful()) {
                throw new IOException("API request failed: " + response.code());
            }

            String jsonResponse = response.body().string();
            JsonObject jsonObject = gson.fromJson(jsonResponse, JsonObject.class);
            JsonArray results = jsonObject.getAsJsonArray("results");

            List<Movie> movies = new ArrayList<>();
            for (JsonElement element : results) {
                movies.add(parseMovie(element.getAsJsonObject()));
            }

            return movies;
        }
    }

    @Override
    public MovieDetails getMovieDetails(int movieId) throws IOException {
        String url = baseUrl + "/movie/" + movieId + "?api_key=" + apiKey + "&language=es-ES";
        
        Request request = new Request.Builder()
                .url(url)
                .build();

        try (Response response = client.newCall(request).execute()) {
            if (!response.isSuccessful()) {
                throw new IOException("API request failed: " + response.code());
            }

            String jsonResponse = response.body().string();
            JsonObject jsonObject = gson.fromJson(jsonResponse, JsonObject.class);
            
            return parseMovieDetails(jsonObject);
        }
    }

    private Movie parseMovie(JsonObject json) {
        int id = json.get("id").getAsInt();
        String title = json.get("title").getAsString();
        String overview = getStringOrDefault(json, "overview", "");
        String posterPath = getStringOrDefault(json, "poster_path", null);
        String releaseDate = getStringOrDefault(json, "release_date", "");
        double voteAverage = getDoubleOrDefault(json, "vote_average", 0.0);
        int voteCount = getIntOrDefault(json, "vote_count", 0);

        return new Movie(id, title, overview, posterPath, releaseDate, voteAverage, voteCount);
    }

    private MovieDetails parseMovieDetails(JsonObject json) {
        int id = json.get("id").getAsInt();
        String title = json.get("title").getAsString();
        String overview = getStringOrDefault(json, "overview", "");
        String posterPath = getStringOrDefault(json, "poster_path", null);
        String releaseDate = getStringOrDefault(json, "release_date", "");
        double voteAverage = getDoubleOrDefault(json, "vote_average", 0.0);
        int voteCount = getIntOrDefault(json, "vote_count", 0);

        // Additional details
        List<String> genres = parseGenres(json.getAsJsonArray("genres"));
        int runtime = getIntOrDefault(json, "runtime", 0);
        String tagline = getStringOrDefault(json, "tagline", "");
        long budget = getLongOrDefault(json, "budget", 0);
        long revenue = getLongOrDefault(json, "revenue", 0);

        return new MovieDetails(id, title, overview, posterPath, releaseDate, 
                               voteAverage, voteCount, genres, runtime, tagline, 
                               budget, revenue);
    }

    private List<String> parseGenres(JsonArray genresArray) {
        List<String> genres = new ArrayList<>();
        if (genresArray != null) {
            for (JsonElement element : genresArray) {
                JsonObject genreObj = element.getAsJsonObject();
                genres.add(genreObj.get("name").getAsString());
            }
        }
        return genres;
    }

    private String getStringOrDefault(JsonObject json, String key, String defaultValue) {
        JsonElement element = json.get(key);
        if (element == null || element.isJsonNull()) {
            return defaultValue;
        }
        return element.getAsString();
    }

    private int getIntOrDefault(JsonObject json, String key, int defaultValue) {
        JsonElement element = json.get(key);
        if (element == null || element.isJsonNull()) {
            return defaultValue;
        }
        return element.getAsInt();
    }

    private long getLongOrDefault(JsonObject json, String key, long defaultValue) {
        JsonElement element = json.get(key);
        if (element == null || element.isJsonNull()) {
            return defaultValue;
        }
        return element.getAsLong();
    }

    private double getDoubleOrDefault(JsonObject json, String key, double defaultValue) {
        JsonElement element = json.get(key);
        if (element == null || element.isJsonNull()) {
            return defaultValue;
        }
        return element.getAsDouble();
    }
}

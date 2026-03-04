package es.unican.ucred.movies.ui;

import es.unican.ucred.movies.model.Movie;
import es.unican.ucred.movies.model.MovieDetails;
import es.unican.ucred.movies.service.ITMDbService;
import org.assertj.swing.core.GenericTypeMatcher;
import org.assertj.swing.edt.FailOnThreadViolationRepaintManager;
import org.assertj.swing.edt.GuiActionRunner;
import org.assertj.swing.fixture.FrameFixture;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import javax.swing.*;
import java.io.IOException;
import java.util.Arrays;
import java.util.List;

import static org.mockito.ArgumentMatchers.anyInt;
import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.when;

class MainFrameTest {
    private FrameFixture window;
    private ITMDbService mockService;
    private MainFrame frame;

    @BeforeAll
    static void setUpOnce() {
        FailOnThreadViolationRepaintManager.install();
    }

    @BeforeEach
    void setUp() throws IOException {
        // Create mock service with test data
        mockService = mock(ITMDbService.class);
        
        List<Movie> mockMovies = Arrays.asList(
                new Movie(1, "Test Movie 1", "Overview 1", "/poster1.jpg", 
                         "2020-01-01", 8.5, 1000),
                new Movie(2, "Test Movie 2", "Overview 2", "/poster2.jpg", 
                         "2021-02-15", 7.3, 500)
        );

        MovieDetails mockDetails = new MovieDetails(
                1, "Test Movie 1", "Detailed overview", "/poster1.jpg",
                "2020-01-01", 8.5, 1000,
                Arrays.asList("Action", "Drama"), 120, "Epic tagline",
                1000000, 5000000
        );

        when(mockService.getPopularMovies()).thenReturn(mockMovies);
        when(mockService.getMovieDetails(anyInt())).thenReturn(mockDetails);

        // Create frame on EDT
        frame = GuiActionRunner.execute(() -> new MainFrame(mockService));
        window = new FrameFixture(frame);
        window.show();
    }

    @AfterEach
    void tearDown() {
        window.cleanUp();
    }

    @Test
    void shouldDisplayMainFrameTitle() {
        window.requireTitle("UCRED Movies Browser");
    }

    @Test
    void shouldLoadMoviesOnStartup() throws InterruptedException {
        // Wait for async loading
        Thread.sleep(500);
        
        // Should display status message
        window.label(new GenericTypeMatcher<JLabel>(JLabel.class) {
            @Override
            protected boolean isMatching(JLabel label) {
                String text = label.getText();
                return text != null && text.contains("películas");
            }
        }).requireVisible();
    }

    @Test
    void shouldDisplayMovieGrid() throws InterruptedException {
        // Wait for async loading
        Thread.sleep(500);
        
        // Panel should be visible
        window.panel("MovieGridPanel").requireVisible();
    }

    @Test
    void shouldDisplayDetailsPanel() {
        window.panel("MovieDetailsPanel").requireVisible();
    }

    @Test
    void frameIsResizable() {
        window.requireVisible();
        frame.setResizable(true);
        window.resizeTo(new java.awt.Dimension(1200, 800));
    }
}

package es.unican.ucred.movies.ui;

import es.unican.ucred.movies.model.Movie;
import es.unican.ucred.movies.model.MovieDetails;
import es.unican.ucred.movies.service.ITMDbService;

import javax.swing.*;
import java.awt.*;
import java.io.IOException;
import java.util.List;

/**
 * Main application frame for the movie browser.
 */
public class MainFrame extends JFrame {
    private final ITMDbService tmdbService;
    private final MovieGridPanel gridPanel;
    private final MovieDetailsPanel detailsPanel;

    public MainFrame(ITMDbService tmdbService) {
        this.tmdbService = tmdbService;
        
        setTitle("UCRED Movies Browser");
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setLayout(new BorderLayout());

        // Create panels
        gridPanel = new MovieGridPanel();
        detailsPanel = new MovieDetailsPanel();

        // Set up grid selection handler
        gridPanel.setOnMovieSelected(this::onMovieSelected);

        // Create split pane
        JScrollPane gridScroll = new JScrollPane(gridPanel);
        gridScroll.setPreferredSize(new Dimension(700, 600));
        
        JSplitPane splitPane = new JSplitPane(
                JSplitPane.HORIZONTAL_SPLIT,
                gridScroll,
                detailsPanel);
        splitPane.setDividerLocation(700);
        splitPane.setResizeWeight(0.7);

        add(splitPane, BorderLayout.CENTER);

        // Create status bar
        JLabel statusBar = new JLabel("Cargando películas populares...");
        statusBar.setBorder(BorderFactory.createEmptyBorder(5, 10, 5, 10));
        add(statusBar, BorderLayout.SOUTH);

        pack();
        setLocationRelativeTo(null);

        // Load popular movies asynchronously
        SwingWorker<List<Movie>, Void> worker = new SwingWorker<>() {
            @Override
            protected List<Movie> doInBackground() throws Exception {
                return tmdbService.getPopularMovies();
            }

            @Override
            protected void done() {
                try {
                    List<Movie> movies = get();
                    gridPanel.displayMovies(movies);
                    statusBar.setText("Mostrando " + movies.size() + " películas populares");
                } catch (Exception e) {
                    showError("Error al cargar películas: " + e.getMessage());
                    statusBar.setText("Error al cargar películas");
                }
            }
        };
        worker.execute();
    }

    private void onMovieSelected(Movie movie) {
        // Load movie details asynchronously
        SwingWorker<MovieDetails, Void> worker = new SwingWorker<>() {
            @Override
            protected MovieDetails doInBackground() throws Exception {
                return tmdbService.getMovieDetails(movie.getId());
            }

            @Override
            protected void done() {
                try {
                    MovieDetails details = get();
                    detailsPanel.showDetails(details);
                } catch (Exception e) {
                    showError("Error al cargar detalles: " + e.getMessage());
                }
            }
        };
        worker.execute();
    }

    private void showError(String message) {
        JOptionPane.showMessageDialog(
                this,
                message,
                "Error",
                JOptionPane.ERROR_MESSAGE);
    }
}

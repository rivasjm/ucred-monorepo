package es.unican.ucred.movies.ui;

import es.unican.ucred.movies.model.Movie;
import es.unican.ucred.movies.util.ImageLoader;

import javax.swing.*;
import java.awt.*;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.util.List;
import java.util.function.Consumer;

/**
 * Panel that displays a grid of movie posters.
 */
public class MovieGridPanel extends JPanel {
    private static final int COLUMNS = 4;
    private static final int CELL_WIDTH = 160;
    private static final int CELL_HEIGHT = 280;
    
    private Consumer<Movie> onMovieSelected;
    private List<Movie> movies;

    public MovieGridPanel() {
        setName("MovieGridPanel");
        setLayout(new GridLayout(0, COLUMNS, 10, 10));
        setBorder(BorderFactory.createEmptyBorder(10, 10, 10, 10));
    }

    /**
     * Set the callback to be invoked when a movie is selected.
     * @param callback Callback that receives the selected movie
     */
    public void setOnMovieSelected(Consumer<Movie> callback) {
        this.onMovieSelected = callback;
    }

    /**
     * Display a list of movies in the grid.
     * @param movies List of movies to display
     */
    public void displayMovies(List<Movie> movies) {
        this.movies = movies;
        removeAll();

        if (movies == null || movies.isEmpty()) {
            add(new JLabel("No hay películas para mostrar"));
            revalidate();
            repaint();
            return;
        }

        for (Movie movie : movies) {
            add(createMovieCard(movie));
        }

        revalidate();
        repaint();
    }

    private JPanel createMovieCard(Movie movie) {
        JPanel card = new JPanel();
        card.setLayout(new BorderLayout(5, 5));
        card.setPreferredSize(new Dimension(CELL_WIDTH, CELL_HEIGHT));
        card.setBorder(BorderFactory.createLineBorder(Color.LIGHT_GRAY, 1));
        card.setCursor(Cursor.getPredefinedCursor(Cursor.HAND_CURSOR));

        // Poster
        JLabel posterLabel = new JLabel();
        posterLabel.setHorizontalAlignment(SwingConstants.CENTER);
        ImageLoader.loadImageAsync(movie.getPosterUrl(), posterLabel);
        card.add(posterLabel, BorderLayout.CENTER);

        // Title and rating
        JPanel infoPanel = new JPanel();
        infoPanel.setLayout(new BoxLayout(infoPanel, BoxLayout.Y_AXIS));
        
        JLabel titleLabel = new JLabel(truncate(movie.getTitle(), 20));
        titleLabel.setFont(new Font("Arial", Font.BOLD, 11));
        titleLabel.setAlignmentX(Component.CENTER_ALIGNMENT);
        
        JLabel ratingLabel = new JLabel("⭐ " + movie.getRatingString());
        ratingLabel.setFont(new Font("Arial", Font.PLAIN, 10));
        ratingLabel.setAlignmentX(Component.CENTER_ALIGNMENT);
        
        infoPanel.add(titleLabel);
        infoPanel.add(ratingLabel);
        card.add(infoPanel, BorderLayout.SOUTH);

        // Click handler
        card.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                if (onMovieSelected != null) {
                    onMovieSelected.accept(movie);
                }
            }

            @Override
            public void mouseEntered(MouseEvent e) {
                card.setBorder(BorderFactory.createLineBorder(Color.BLUE, 2));
            }

            @Override
            public void mouseExited(MouseEvent e) {
                card.setBorder(BorderFactory.createLineBorder(Color.LIGHT_GRAY, 1));
            }
        });

        return card;
    }

    private String truncate(String text, int maxLength) {
        if (text.length() <= maxLength) {
            return text;
        }
        return text.substring(0, maxLength - 3) + "...";
    }
}

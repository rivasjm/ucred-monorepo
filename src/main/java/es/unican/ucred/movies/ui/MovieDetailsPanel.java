package es.unican.ucred.movies.ui;

import es.unican.ucred.movies.model.MovieDetails;

import javax.swing.*;
import java.awt.*;

/**
 * Panel that displays detailed information about a selected movie.
 */
public class MovieDetailsPanel extends JPanel {
    private final JLabel posterLabel;
    private final JLabel titleLabel;
    private final JLabel yearLabel;
    private final JLabel ratingLabel;
    private final JLabel genresLabel;
    private final JLabel runtimeLabel;
    private final JLabel taglineLabel;
    private final JTextArea overviewArea;
    private final JLabel budgetLabel;
    private final JLabel revenueLabel;

    public MovieDetailsPanel() {
        setName("MovieDetailsPanel");
        setLayout(new BorderLayout(10, 10));
        setBorder(BorderFactory.createEmptyBorder(10, 10, 10, 10));
        setPreferredSize(new Dimension(400, 600));

        // Poster
        posterLabel = new JLabel();
        posterLabel.setHorizontalAlignment(SwingConstants.CENTER);
        add(posterLabel, BorderLayout.NORTH);

        // Details panel
        JPanel detailsPanel = new JPanel();
        detailsPanel.setLayout(new BoxLayout(detailsPanel, BoxLayout.Y_AXIS));
        
        titleLabel = createLabel("", 20, Font.BOLD);
        yearLabel = createLabel("", 14, Font.PLAIN);
        ratingLabel = createLabel("", 14, Font.BOLD);
        genresLabel = createLabel("", 12, Font.ITALIC);
        runtimeLabel = createLabel("", 12, Font.PLAIN);
        taglineLabel = createLabel("", 11, Font.ITALIC);
        taglineLabel.setForeground(Color.GRAY);

        detailsPanel.add(titleLabel);
        detailsPanel.add(Box.createVerticalStrut(5));
        detailsPanel.add(yearLabel);
        detailsPanel.add(Box.createVerticalStrut(5));
        detailsPanel.add(ratingLabel);
        detailsPanel.add(Box.createVerticalStrut(5));
        detailsPanel.add(genresLabel);
        detailsPanel.add(Box.createVerticalStrut(5));
        detailsPanel.add(runtimeLabel);
        detailsPanel.add(Box.createVerticalStrut(10));
        detailsPanel.add(taglineLabel);
        detailsPanel.add(Box.createVerticalStrut(10));

        // Overview
        overviewArea = new JTextArea();
        overviewArea.setLineWrap(true);
        overviewArea.setWrapStyleWord(true);
        overviewArea.setEditable(false);
        overviewArea.setFont(new Font("Arial", Font.PLAIN, 12));
        overviewArea.setBorder(BorderFactory.createTitledBorder("Sinopsis"));
        JScrollPane overviewScroll = new JScrollPane(overviewArea);
        overviewScroll.setPreferredSize(new Dimension(380, 150));
        detailsPanel.add(overviewScroll);
        detailsPanel.add(Box.createVerticalStrut(10));

        // Financial info
        budgetLabel = createLabel("", 11, Font.PLAIN);
        revenueLabel = createLabel("", 11, Font.PLAIN);
        detailsPanel.add(budgetLabel);
        detailsPanel.add(Box.createVerticalStrut(3));
        detailsPanel.add(revenueLabel);

        JScrollPane detailsScroll = new JScrollPane(detailsPanel);
        add(detailsScroll, BorderLayout.CENTER);

        showPlaceholder();
    }

    /**
     * Display detailed information about a movie.
     * @param details Movie details to display
     */
    public void showDetails(MovieDetails details) {
        if (details == null) {
            showPlaceholder();
            return;
        }

        titleLabel.setText(details.getTitle());
        yearLabel.setText("Año: " + details.getYear());
        ratingLabel.setText("⭐ " + details.getRatingString() + " (" + details.getVoteCount() + " votos)");
        genresLabel.setText("Géneros: " + details.getGenresString());
        runtimeLabel.setText("Duración: " + details.getRuntimeString());
        taglineLabel.setText("\"" + details.getTagline() + "\"");
        overviewArea.setText(details.getOverview());
        budgetLabel.setText("Presupuesto: " + details.getBudgetString());
        revenueLabel.setText("Recaudación: " + details.getRevenueString());

        // Load poster
        es.unican.ucred.movies.util.ImageLoader.loadImageAsync(
                details.getLargePosterUrl(), posterLabel);
    }

    /**
     * Show a placeholder message when no movie is selected.
     */
    private void showPlaceholder() {
        titleLabel.setText("Selecciona una película");
        yearLabel.setText("");
        ratingLabel.setText("");
        genresLabel.setText("");
        runtimeLabel.setText("");
        taglineLabel.setText("");
        overviewArea.setText("");
        budgetLabel.setText("");
        revenueLabel.setText("");
        posterLabel.setIcon(null);
    }

    private JLabel createLabel(String text, int fontSize, int fontStyle) {
        JLabel label = new JLabel(text);
        label.setFont(new Font("Arial", fontStyle, fontSize));
        return label;
    }
}

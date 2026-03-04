package es.unican.ucred.movies.util;

import javax.swing.*;
import java.awt.*;
import java.net.URL;
import java.util.concurrent.CompletableFuture;

/**
 * Utility class for asynchronously loading images from URLs.
 */
public class ImageLoader {
    private static final int POSTER_WIDTH = 150;
    private static final int POSTER_HEIGHT = 225;
    private static final Image PLACEHOLDER = createPlaceholder();

    /**
     * Asynchronously loads an image from a URL and sets it on a JLabel.
     * @param url The image URL
     * @param label The JLabel to display the image
     */
    public static void loadImageAsync(String url, JLabel label) {
        if (url == null || url.isEmpty()) {
            label.setIcon(new ImageIcon(PLACEHOLDER));
            return;
        }

        // Set placeholder while loading
        label.setIcon(new ImageIcon(PLACEHOLDER));

        CompletableFuture.runAsync(() -> {
            try {
                URL imageUrl = new URL(url);
                ImageIcon icon = new ImageIcon(imageUrl);
                Image image = icon.getImage().getScaledInstance(
                        POSTER_WIDTH, POSTER_HEIGHT, Image.SCALE_SMOOTH);
                
                SwingUtilities.invokeLater(() -> {
                    label.setIcon(new ImageIcon(image));
                });
            } catch (Exception e) {
                SwingUtilities.invokeLater(() -> {
                    label.setIcon(new ImageIcon(PLACEHOLDER));
                });
            }
        });
    }

    /**
     * Creates an image placeholder for movies without posters.
     * @return Placeholder image
     */
    private static Image createPlaceholder() {
        java.awt.image.BufferedImage image = new java.awt.image.BufferedImage(
                POSTER_WIDTH, POSTER_HEIGHT, java.awt.image.BufferedImage.TYPE_INT_RGB);
        Graphics2D g2d = image.createGraphics();
        
        // Gray background
        g2d.setColor(new Color(200, 200, 200));
        g2d.fillRect(0, 0, POSTER_WIDTH, POSTER_HEIGHT);
        
        // "No Image" text
        g2d.setColor(new Color(100, 100, 100));
        g2d.setFont(new Font("Arial", Font.BOLD, 14));
        FontMetrics fm = g2d.getFontMetrics();
        String text = "No Image";
        int x = (POSTER_WIDTH - fm.stringWidth(text)) / 2;
        int y = POSTER_HEIGHT / 2;
        g2d.drawString(text, x, y);
        
        g2d.dispose();
        return image;
    }
}

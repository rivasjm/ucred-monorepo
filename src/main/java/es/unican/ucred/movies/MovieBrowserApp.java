package es.unican.ucred.movies;

import es.unican.ucred.movies.service.ITMDbService;
import es.unican.ucred.movies.service.TMDbService;
import es.unican.ucred.movies.ui.MainFrame;

import javax.swing.*;
import java.awt.*;

/**
 * Main application class for the UCRED Movies Browser.
 */
public class MovieBrowserApp {
    public static void main(String[] args) {
        // Set system look and feel
        try {
            UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
        } catch (Exception e) {
            // Use default look and feel
        }

        // Launch UI on Event Dispatch Thread
        SwingUtilities.invokeLater(() -> {
            // Show API key dialog
            String apiKey = showApiKeyDialog();
            
            if (apiKey == null || apiKey.trim().isEmpty()) {
                JOptionPane.showMessageDialog(
                    null,
                    "La aplicación requiere una API key de TMDb para funcionar.\n" +
                    "Obtén tu API key gratis en:\n" +
                    "https://www.themoviedb.org/settings/api",
                    "API Key Requerida",
                    JOptionPane.WARNING_MESSAGE
                );
                System.exit(0);
                return;
            }

            // Create service with the provided API key
            ITMDbService tmdbService = new TMDbService(apiKey);

            // Create and show main frame
            MainFrame frame = new MainFrame(tmdbService);
            frame.setVisible(true);
        });
    }

    /**
     * Shows a dialog to request the TMDb API key from the user.
     * 
     * @return The API key entered by the user, or null if cancelled
     */
    private static String showApiKeyDialog() {
        JPanel panel = new JPanel();
        panel.setLayout(new BoxLayout(panel, BoxLayout.Y_AXIS));
        
        JLabel titleLabel = new JLabel("API Key de TMDb");
        titleLabel.setFont(titleLabel.getFont().deriveFont(Font.BOLD, 16f));
        titleLabel.setAlignmentX(Component.LEFT_ALIGNMENT);
        
        JLabel infoLabel = new JLabel("<html>" +
            "<p>Para usar esta aplicación necesitas una API key de<br>" +
            "The Movie Database (TMDb). Es <b>gratis</b> y tarda 1 minuto.</p><br>" +
            "<p>Obtén tu API key en:<br>" +
            "<font color='blue'>https://www.themoviedb.org/settings/api</font></p>" +
            "</html>");
        infoLabel.setAlignmentX(Component.LEFT_ALIGNMENT);
        
        JTextField apiKeyField = new JTextField(40);
        apiKeyField.setAlignmentX(Component.LEFT_ALIGNMENT);
        apiKeyField.setMaximumSize(new Dimension(400, 25));
        
        JLabel fieldLabel = new JLabel("Introduce tu API key:");
        fieldLabel.setAlignmentX(Component.LEFT_ALIGNMENT);
        
        panel.add(titleLabel);
        panel.add(Box.createVerticalStrut(10));
        panel.add(infoLabel);
        panel.add(Box.createVerticalStrut(15));
        panel.add(fieldLabel);
        panel.add(Box.createVerticalStrut(5));
        panel.add(apiKeyField);
        panel.setBorder(BorderFactory.createEmptyBorder(10, 10, 10, 10));
        
        int result = JOptionPane.showConfirmDialog(
            null,
            panel,
            "UCRED Movies Browser - Configuración",
            JOptionPane.OK_CANCEL_OPTION,
            JOptionPane.PLAIN_MESSAGE
        );
        
        if (result == JOptionPane.OK_OPTION) {
            return apiKeyField.getText().trim();
        }
        
        return null;
    }
}

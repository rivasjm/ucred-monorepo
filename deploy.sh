#!/bin/bash
set -e

echo "📤 Subiendo archivos a WebDAV (subdirectorio calculadora)..."

# Create calculadora directory if it doesn't exist
curl -u "$WEBDAV_USERNAME:$WEBDAV_PASSWORD" \
     -X MKCOL "$WEBDAV_URL/calculadora" || true

# Upload index.html
curl -u "$WEBDAV_USERNAME:$WEBDAV_PASSWORD" \
     -T index.html "$WEBDAV_URL/calculadora/index.html"
echo "✅ index.html uploaded"

# Create css directory
curl -u "$WEBDAV_USERNAME:$WEBDAV_PASSWORD" \
     -X MKCOL "$WEBDAV_URL/calculadora/css" || true

# Upload CSS files
for file in css/*.css; do
  if [ -f "$file" ]; then
    filename=$(basename "$file")
    curl -u "$WEBDAV_USERNAME:$WEBDAV_PASSWORD" \
         -T "$file" "$WEBDAV_URL/calculadora/css/$filename"
    echo "✅ $file uploaded"
  fi
done

# Create js directory
curl -u "$WEBDAV_USERNAME:$WEBDAV_PASSWORD" \
     -X MKCOL "$WEBDAV_URL/calculadora/js" || true

# Upload JS files
for file in js/*.js; do
  if [ -f "$file" ]; then
    filename=$(basename "$file")
    curl -u "$WEBDAV_USERNAME:$WEBDAV_PASSWORD" \
         -T "$file" "$WEBDAV_URL/calculadora/js/$filename"
    echo "✅ $file uploaded"
  fi
done

echo "🎉 Deployment completado en /calculadora!"

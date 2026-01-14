# RentaVehiculos

## Docker

Build the Docker image locally:

```bash
docker build -t allanchopen/rentavehiculos:latest .
```

Run the container locally (maps port 8080):

```bash
docker run -p 8080:8080 -e ASPNETCORE_ENVIRONMENT=Development \
	-e ConnectionStrings__Default="<Your_Connection_String>" \
	allanchopen/rentavehiculos:latest
```

Deploying to Koyeb:
- Push the image to a registry (Docker Hub or GitHub Container Registry).
- In Koyeb, create a new app and point it to the container image.
- Set environment variables via Koyeb UI (for example the connection string and any secrets).

Notes:
- The container listens on port `8080` (set by `ASPNETCORE_URLS`).
- Do not store production secrets in the image — provide them through Koyeb secrets or environment variables.


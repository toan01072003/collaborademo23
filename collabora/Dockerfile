FROM collabora/code:24.04
ENV extra_params=--o:ssl.enable=false
# ⚠️ tighten after first deploy
ENV domain=.+
COPY start-collabora.sh /app/start-collabora.sh
RUN chmod +x /app/start-collabora.sh
EXPOSE 9980

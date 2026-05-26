module.exports = {
  devServer: {
    port: 3004,
    allowedHosts: [
      'localhost',
      '127.0.0.1',
      '0.0.0.0',
      'logistics.yaaniai.com'
    ],
    proxy: {
      '/api': {
        target: 'http://localhost:8003',
        changeOrigin: true
      }
    }
  }
}

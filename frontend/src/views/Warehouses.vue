<template>
  <div>
    <h1>Warehouses</h1>
    <div class="stats-grid">
      <div class="card" v-for="w in warehouses" :key="w.id">
        <h3>{{ w.name }}</h3>
        <p><strong>City:</strong> {{ w.city }}</p>
        <p><strong>Capacity:</strong> {{ w.used.toLocaleString() }} / {{ w.capacity.toLocaleString() }}</p>
        <div style="background:#eee;border-radius:8px;height:8px;margin:8px 0">
          <div :style="{width: (w.used/w.capacity*100)+'%', background: w.used/w.capacity > 0.8 ? '#e74c3c' : '#4361ee', height:'8px', borderRadius:'8px'}"></div>
        </div>
        <p style="font-size:12px;color:#666">{{ (w.used/w.capacity*100).toFixed(0) }}% utilized · Manager: {{ w.manager }}</p>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() { return { warehouses: [] } },
  async created() {
    this.warehouses = (await this.$http.get('/api/warehouses')).data
  }
}
</script>
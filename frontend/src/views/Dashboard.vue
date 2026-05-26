<template>
  <div>
    <h1>Dashboard</h1>
    <div class="stats-grid">
      <div class="card stat-card" v-for="s in statCards" :key="s.label">
        <div class="value">{{ s.value }}</div>
        <div class="label">{{ s.label }}</div>
      </div>
    </div>
    <div class="stats-grid">
      <div class="card">
        <h3>Daily Shipments (Last 7 days)</h3>
        <table>
          <thead><tr><th>Date</th><th>Count</th><th>Revenue</th></tr></thead>
          <tbody><tr v-for="d in daily" :key="d.date"><td>{{ d.date }}</td><td>{{ d.count }}</td><td>₹{{ d.revenue.toLocaleString() }}</td></tr></tbody>
        </table>
      </div>
    </div>
    <div class="stats-grid" style="margin-top:16px">
      <div class="card">
        <h3>Recent Shipments</h3>
        <table>
          <thead><tr><th>ID</th><th>Route</th><th>Status</th><th>ETA</th></tr></thead>
          <tbody><tr v-for="s in shipments.slice(0,5)" :key="s.id">
            <td>{{ s.id }}</td><td>{{ s.origin }} → {{ s.destination }}</td>
            <td><span :class="statusClass(s.status)">{{ s.status }}</span></td>
            <td>{{ s.eta }}</td>
          </tr></tbody>
        </table>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() { return { stats: {}, shipments: [], daily: [] } },
  async created() {
    const [stats, shipments, daily] = await Promise.all([
      this.$http.get('/api/stats').then(r => r.data),
      this.$http.get('/api/shipments').then(r => r.data),
      this.$http.get('/api/daily').then(r => r.data),
    ])
    this.stats = stats; this.shipments = shipments; this.daily = daily
  },
  computed: {
    statCards() {
      const s = this.stats
      if (!s.totalShipments) return []
      return [
        { value: s.totalShipments.toLocaleString(), label: 'Total Shipments' },
        { value: s.inTransit, label: 'In Transit' },
        { value: s.delivered, label: 'Delivered' },
        { value: s.onTimeRate + '%', label: 'On-Time Rate' },
        { value: '₹' + (s.totalRevenue / 100000).toFixed(1) + 'L', label: 'Revenue' },
        { value: s.avgDeliveryDays + ' days', label: 'Avg Delivery' },
      ]
    }
  },
  methods: {
    statusClass(s) {
      return 'badge ' + ({ Delivered: 'badge-green', 'In Transit': 'badge-blue', Pending: 'badge-yellow', Delayed: 'badge-red' }[s] || '')
    }
  }
}
</script>
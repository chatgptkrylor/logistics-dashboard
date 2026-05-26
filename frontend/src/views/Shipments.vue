<template>
  <div>
    <h1>Shipments</h1>
    <div class="card">
      <table>
        <thead><tr><th>ID</th><th>Origin</th><th>Destination</th><th>Carrier</th><th>Weight</th><th>Cost</th><th>Status</th><th>ETA</th></tr></thead>
        <tbody><tr v-for="s in shipments" :key="s.id">
          <td><strong>{{ s.id }}</strong></td><td>{{ s.origin }}</td><td>{{ s.destination }}</td>
          <td>{{ s.carrier }}</td><td>{{ s.weight }} kg</td><td>₹{{ s.cost.toLocaleString() }}</td>
          <td><span :class="statusClass(s.status)">{{ s.status }}</span></td><td>{{ s.eta }}</td>
        </tr></tbody>
      </table>
    </div>
  </div>
</template>

<script>
export default {
  data() { return { shipments: [] } },
  async created() {
    this.shipments = (await this.$http.get('/api/shipments')).data
  },
  methods: {
    statusClass(s) {
      return 'badge ' + ({ Delivered: 'badge-green', 'In Transit': 'badge-blue', Pending: 'badge-yellow', Delayed: 'badge-red' }[s] || '')
    }
  }
}
</script>
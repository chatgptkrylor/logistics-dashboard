<template>
  <div>
    <h1>Vehicles</h1>
    <div class="card">
      <table>
        <thead><tr><th>ID</th><th>Type</th><th>Plate</th><th>Driver</th><th>Route</th><th>Fuel</th><th>Status</th></tr></thead>
        <tbody><tr v-for="v in vehicles" :key="v.id">
          <td><strong>{{ v.id }}</strong></td><td>{{ v.type }}</td><td>{{ v.plate }}</td><td>{{ v.driver }}</td>
          <td>{{ v.route }}</td><td>{{ v.fuel }}%</td>
          <td><span :class="'badge ' + ({ Active:'badge-green', Idle:'badge-yellow', Maintenance:'badge-red' }[v.status]||'')">{{ v.status }}</span></td>
        </tr></tbody>
      </table>
    </div>
  </div>
</template>

<script>
export default {
  data() { return { vehicles: [] } },
  async created() {
    this.vehicles = (await this.$http.get('/api/vehicles')).data
  }
}
</script>
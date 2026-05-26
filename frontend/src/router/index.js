import Vue from 'vue'
import VueRouter from 'vue-router'
import Dashboard from '../views/Dashboard.vue'
import Shipments from '../views/Shipments.vue'
import Warehouses from '../views/Warehouses.vue'
import Vehicles from '../views/Vehicles.vue'

Vue.use(VueRouter)

const routes = [
  { path: '/', name: 'Dashboard', component: Dashboard },
  { path: '/shipments', name: 'Shipments', component: Shipments },
  { path: '/warehouses', name: 'Warehouses', component: Warehouses },
  { path: '/vehicles', name: 'Vehicles', component: Vehicles },
]

export default new VueRouter({ mode: 'history', routes })
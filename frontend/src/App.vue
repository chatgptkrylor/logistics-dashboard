<template>
  <div id="app">
    <nav class="sidebar">
      <h2>📦 Logistics</h2>
      <router-link to="/">Dashboard</router-link>
      <router-link to="/shipments">Shipments</router-link>
      <router-link to="/warehouses">Warehouses</router-link>
      <router-link to="/vehicles">Vehicles</router-link>
    </nav>
    <main class="content">
      <router-view/>
    </main>
  </div>
</template>

<style>
* { margin: 0; padding: 0; box-sizing: border-box; }
body { font-family: 'Segoe UI', system-ui, sans-serif; background: #f0f2f5; color: #1a1a2e; }
#app { display: flex; min-height: 100vh; }
.sidebar { width: 220px; background: #1a1a2e; color: #fff; padding: 24px 16px; display: flex; flex-direction: column; gap: 8px; }
.sidebar h2 { margin-bottom: 24px; font-size: 20px; }
.sidebar a { color: #a0a0b8; text-decoration: none; padding: 10px 12px; border-radius: 8px; transition: all .2s; }
.sidebar a:hover { background: #2d2d44; color: #fff; }
.sidebar a.router-link-exact-active { background: #4361ee; color: #fff; }
.content { flex: 1; padding: 24px; overflow-y: auto; }
.card { background: #fff; border-radius: 12px; padding: 20px; box-shadow: 0 1px 4px rgba(0,0,0,.08); }
.stats-grid { display: grid; grid-template-columns: repeat(auto-fit, minmax(200px, 1fr)); gap: 16px; margin-bottom: 24px; }
.stat-card { text-align: center; }
.stat-card .value { font-size: 28px; font-weight: 700; color: #4361ee; }
.stat-card .label { font-size: 13px; color: #666; margin-top: 4px; }
table { width: 100%; border-collapse: collapse; }
th, td { padding: 10px 14px; text-align: left; border-bottom: 1px solid #eee; font-size: 14px; }
th { background: #f8f9fa; font-weight: 600; color: #555; }
.badge { padding: 4px 10px; border-radius: 20px; font-size: 12px; font-weight: 600; }
.badge-green { background: #d4edda; color: #155724; }
.badge-blue { background: #d1ecf1; color: #0c5460; }
.badge-yellow { background: #fff3cd; color: #856404; }
.badge-red { background: #f8d7da; color: #721c24; }
h1 { font-size: 22px; margin-bottom: 20px; }
h3 { margin-bottom: 12px; }
</style>
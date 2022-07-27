<template>
  <header>
    <div class="container-fluid">

      <div class="row justify-content-center align-items-center ">
        <div
          class="col-md-10 d-flex flex-column justify-content-center align-items-center text-center shadow mt-3 bg-img ">
          <h3>All-Spice</h3>
          <p>"Recipes handed down from grandma herself!"</p>

        </div>
      </div>

      <div class="row justify-content-center ">
        <div class="col-md-4 d-flex shadow rounded  justify-content-around align-items-center  filter-bar">
          <b>Home</b>
          <b>My Recipes</b>
          <b>Favorites</b>
        </div>
      </div>
    </div>

  </header>
  <main>
    <router-view />
  </main>

</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
export default {
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState)
    }
  }
}
</script>
<style lang="scss">
.bg-img {
  background-image: url("https://images.unsplash.com/photo-1547592180-85f173990554?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=2070&q=80");
  min-height: 40vh;
  border-radius: 1%;
  background-position: center;
  object-fit: fill;

}

.filter-bar {
  min-height: 5vh;
  transform: translateY(-1em);
  z-index: 1;
}

@import "./assets/scss/main.scss";
</style>

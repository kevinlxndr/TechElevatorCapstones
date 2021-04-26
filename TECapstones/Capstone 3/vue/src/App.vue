<template>
  <div id="app">
    <div id="nav">
      <router-link id="home-link" v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;|&nbsp;
      <router-link id="logout-link" v-bind:to="{ name: 'logout' }" v-if="$store.state.token !== ''">Logout</router-link>&nbsp;|&nbsp;
      <router-link id="login-link" v-bind:to="{ name: 'login' }" v-if="$store.state.token === ''">Login</router-link>
    </div>
    <router-view />
  </div>
</template>

<style>
@media (max-width: 800px) {
  #app{
  flex: 100%;
  flex-direction: column;
  box-sizing: border-box;
}
}

#home-link, #logout-link, #login-link  {
  text-decoration: none;
  color: white;
  padding: .5rem;
  font-size: 1.3rem;
}

#home-link:hover, #logout-link:hover, #login-link:hover {
  color: rgb(255, 204, 0);
}
</style>
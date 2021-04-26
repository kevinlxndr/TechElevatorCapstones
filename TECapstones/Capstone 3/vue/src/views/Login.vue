<template>
  <div id="login" class="text-center">
    <h1 class="login-and-register">Please Sign In</h1>
    <form class="form-signin" @submit.prevent="login">
      <div
        class="alert alert-danger"
        role="alert"
        v-if="invalidCredentials"
      >Invalid username and password!</div>
      <div
        class="alert alert-success"
        role="alert"
        v-if="this.$route.query.registration"
      >Thank you for registering, please sign in.</div>
      <label for="username" class="sr-only">Username</label>
      <input
        type="text"
        id="username"
        class="form-control"
        placeholder="Username"
        v-model="user.username"
        required
        autofocus
      />
      <label for="password" class="sr-only">Password</label>
      <input
        type="password"
        id="password"
        class="form-control"
        placeholder="Password"
        v-model="user.password"
        required
      />
      <router-link id="need-account-btn" :to="{ name: 'register' }">Need an account?</router-link>
      <button id="submit-btn" type="submit">Sign in</button>
    </form>
  </div>
</template>

<script>
import authService from "../services/AuthService";

export default {
  name: "login",
  components: {},
  data() {
    return {
      user: {
        username: "",
        password: ""
      },
      invalidCredentials: false
    };
  },
  methods: {
    login() {
      authService
        .login(this.user)
        .then(response => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            this.$router.push("/");
          }
        })
        .catch(error => {
          const response = error.response;

          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    }
  }
};
</script>

<style>
#login {
  background: rgb(53, 53, 53);
  display: flex;
  justify-content: center;
  flex-direction: column;
  width: 50%;
  padding: 1rem;
  border-radius: 5px;
}

.form-signin {
  display: flex;
  justify-content: center;
  flex-direction: column;
}

#username, #password {
  display: flex;
  width: 30%;
  margin-left: 35%;
}

#need-account-btn {
  display: flex;
  color: white;
  padding: 1rem;
  text-decoration: none;
  justify-content: center;
}

#need-account-btn:hover {
  color: rgb(251, 169, 45);
  font-size: 103%;
}

#submit-btn {
  padding: 15px;
  width: 50%;
  margin-left: 25%;
}
</style>
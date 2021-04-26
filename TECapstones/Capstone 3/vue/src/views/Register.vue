<template>
  <div id="register" class="text-center">
    <h1 class="login-and-register">Create Account</h1>
    <form class="form-register" @submit.prevent="register">
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        {{ registrationErrorMsg }}
      </div>
      <label for="name" class="sr-only">Full Name</label>
      <input
        type="text"
        id="name"
        class="form-control"
        placeholder="Name"
        v-model="user.name"
        required
        autofocus
      />
      <label for="username" class="sr-only">Username</label>
      <input
        type="text"
        id="username"
        class="form-control"
        placeholder="Username"
        v-model="user.username"
        required
        
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
      <input
        type="password"
        id="confirmPassword"
        class="form-control"
        placeholder="Confirm Password"
        v-model="user.confirmPassword"
        required
      />
      <div id="are-you-brewer">
        <label for="brewer">Are you a Brewer? </label>
        <input type="checkbox" id="brewer" true-value="brewer" false-value="user" v-model="user.role" />
      </div>
      
      <router-link id="have-an-accnt-btn" :to="{ name: 'login' }">Have an account?</router-link>
      <button id="create-accnt-btn" class="btn btn-lg btn-primary btn-block" type="submit">
        Create Account
      </button>
    </form>
  </div>
</template>

<script>
import authService from '../services/AuthService';

export default {
  name: 'register',
  data() {
    return {
      user: {
        username: '',
        password: '',
        confirmPassword: '',
        role: 'user',
        name: ''
      },
      registrationErrors: false,
      registrationErrorMsg: 'There were problems registering this user.',
    };
  },
  methods: {
    register() {
      if (this.user.password != this.user.confirmPassword) {
        this.registrationErrors = true;
        this.registrationErrorMsg = 'Password & Confirm Password do not match.';
      } else {
        authService
          .register(this.user)
          .then((response) => {
            if (response.status == 201) {
              this.$router.push({
                path: '/login',
                query: { registration: 'success' },
              });
            }
          })
          .catch((error) => {
            const response = error.response;
            this.registrationErrors = true;
            if (response.status === 400) {
              this.registrationErrorMsg = 'Bad Request: Validation Errors';
            }
          });
      }
    },
    clearErrors() {
      this.registrationErrors = false;
      this.registrationErrorMsg = 'There were problems registering this user.';
    },
  },
};
</script>

<style>
#register {
  background: rgb(53, 53, 53);
  display: flex;
  flex-direction: column;
  width: 50%;
  padding: 1rem;
  border-radius: 5px;

}

#are-you-brewer {
  display: flex;
  color: white;
  padding: 1rem;
  margin-top: 10px;
  margin-left: 40%;
}

#name, #username {
  display: flex;
  width: 30%;
  margin-left: 35%;
}

#confirmPassword {
  display: flex;
  width: 30%;
  margin-left: 35%;
}

#have-an-accnt-btn {
  display: flex;
  color: white;
  padding: 1rem;
  text-decoration: none;
  margin-left: 40%;
}

#have-an-accnt-btn:hover {
  color: rgb(251, 169, 45);
  font-size: 103%;
}

#create-accnt-btn {
  padding: 15px;
  width: 50%;
  margin-left: 25%;
}
</style>

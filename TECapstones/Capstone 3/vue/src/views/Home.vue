<template>
  <div class="home">
    <div id="header" >
      <img v-if='!hover' src="../assets/newLogo1.png" @click='hover = true'>
      <img v-if='hover' src="../assets/newLogo2.png" @click='hover = false'>
    </div>
    <div id='main-page'>
      <side-nav class='sidenav'/>
      <content-page id='content'/>
    </div>
  </div>
</template>

<script>
import breweryService from "@/services/BreweryService.js";
import beerService from "@/services/BeerService.js";
import reviewService from "@/services/ReviewService.js";
import FavService from '@/services/FavService.js';

import SideNav from '@/components/SideNav.vue';
import ContentPage from '@/components/ContentPage.vue';

export default {
  name: "home",
  data() {
  return {
    hover: false,
  }
  },
  
  components: {
    SideNav,
    ContentPage
  },
  created(){
    breweryService.getBreweries().then(response => {
      this.$store.state.breweries =  response.data;
    })
    beerService.getBeers().then(response => {
      this.$store.state.beers = response.data;
    })
    reviewService.getBeerReviews().then(response => {
      this.$store.state.beerReviews = response.data;
    })
    reviewService.getBreweryReviews().then(response => {
      this.$store.state.breweryReviews = response.data;
    })
    if(this.loggedOn){
    FavService.getFavorites(this.$store.state.user.userId)
    .then(response =>{
      let array = [];
      response.data.forEach((favorite)=>{
        array.push(favorite.breweryID)
      })
      this.$store.state.userFavorites= array;
    })
    }
  },
  methods:{
    
  },
  computed:{
    loggedOn(){
      return localStorage.getItem("user")!==null
    },
    logo(){
      if(this.hover){
        return "../assets/newLogo2.png";
      }
        return "../assets/newLogo1.png";
    }
  }
}
</script>

<style>

#main-page{
  display: flex;
  justify-content: stretch;
  align-items: stretch;
  height: 73vh;
}

#content{
    flex-basis: 100%;
    display: flex;
    color: black;
    margin-left: 25px;
}
.side-nav{
  height: 95%;
}

</style>

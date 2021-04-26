<template>
<div id='viewBreweries'>
      <div id='favbrewerylist'>
        <div id="brewery" v-for='brewery in favoriteBrewery' :key='brewery.breweryId' @click='goToBrewery(brewery)'>
          <div class="inner-block"> 
            <h2>{{brewery.name}} </h2>
            <h2>{{brewery.city}} {{brewery.phone}}</h2>
            <average-brewery-rating class="avgRating" :number-of-brewery="brewery.breweryId"/>
          </div>
        </div>
      </div>
</div>
</template>

<script>
import AverageBreweryRating from '@/components/AverageBreweryRating.vue'
import ImageService from '@/services/ImageService.js'
export default {
    name: "favorite-breweries",
    components: {
        AverageBreweryRating
    },
    computed: {
        favoriteBrewery() {

            return this.$store.state.breweries.filter(brewery => {
                return this.$store.state.userFavorites.includes(brewery.breweryId);
            }) 

        }
    },
    methods: {
        goToBrewery(brewery)
        {
            this.$store.state.currentBrewery = brewery;
        ImageService.getImage(brewery.breweryId).then( response=> {
          localStorage.setItem("breweryPicture",response.data.breweryImgPath)
          this.$store.commit('SET_CURRENT_PAGE', 5)
        })
        }
    }
}
</script>

<style>
#favbrewerylist{
  width: 100%;
}
#fav-breweries
{
    border: 3px solid black;
    margin-bottom: 10px;
    border-radius: 10px;
    background-color: white;
    color: black;
}
</style>
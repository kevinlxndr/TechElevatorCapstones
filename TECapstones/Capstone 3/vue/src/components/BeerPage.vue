<template>
  <div id="beer-page">
    <div class="inner-block">
    <h1>{{beer.name}}</h1>
    <img class="beer-page-picture" :src="getPicture(beer.beerId - 1)"/>
        <h2>Location</h2>
        <h4>{{$store.state.breweries.filter(brewery => {
          return brewery.breweryId === beer.breweryId})[0].name
          }}
        </h4>
        <h2>Beer Description</h2>
        <p>{{beer.description}}</p>
        <h2>Ratings and Reviews</h2>
        <review-display :review-id='beer.beerId' :review-type='true'/>
    </div>
  </div>
</template>

<script>
import ReviewDisplay from '@/components/ReviewDisplay.vue'
export default {
  components: { 
      ReviewDisplay 
      },
  computed: {
      beer(){
        return this.$store.state.beers.filter(beer =>beer.beerId === this.$store.state.currentBeer.beerId)[0]
      }
  },
  methods:{
      getPicture(id){
        let images = require.context("../images/",false)
        return images('./'+this.$store.state.beerPictures[id])
      }
  }
}
</script>

<style>
#beer-page
{
  display: flex;
  padding: 1rem;
  justify-content: center;
    background-color: rgba(53, 53, 53, 0.8);
  border: 2px solid black;
  border-radius: 10px;
  width:100%;
  box-shadow: 5px 5px 3px black;
}

p
{
  padding-left: 30px;
  font-size:1.25rem;
  text-align: center;
}

h1
{
margin-left: 10px;
}
h2{
  font-size:2rem;
  margin-left: 20px;
  display: flex;
  justify-content: center;
  text-align: center;

}

h4
{
  font-size:1.5rem;
  margin-left: 20px;
}
.inner-block{
  height:auto;
  overflow: auto;
  
}
.beer-page-picture{
  display: flex;
  align-self:center;
  height: 200px;
  width: 200px;
}
</style>
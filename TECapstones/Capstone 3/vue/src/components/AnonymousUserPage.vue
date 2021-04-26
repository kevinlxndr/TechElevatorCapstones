ANONYMOUS USER PAGE

<template>
  <div id="main">
    <div id="text">
      <h1>Find the right brew for you!</h1>
      <h2>Search our list of breweries in the Pittsburgh area</h2>
      <p>
        Steel City Brews is your one stop for that bubbly hop pop.
        <br>
        <br>
        Pittsburgh is best known for its rivers and bridges, but did you know that Steel Town is also home to some of the best breweries this side of the Monongahela?
        <br>
        That’s right. You can enjoy dozens of delicious brews and enjoy mind bending views all within the confines of the city.
        From Urban Tap to Southern Tier, you’re in the right place to chug or sip a beer. 
        Does your fading and foggy memory of last night taste like heaven? Then you can logon to Steel City Brews and rate that beer an 11!
        <br><br>
        (Note: Rating system is based off of the 5 star system.)
        <br><br> 
        Meet a pretty gal or guy while searching for that perfect microbrew? 
        <br>
        Add some breweries to your favorites list that they can enjoy too! 
        Don’t forget to leave a review.
      </p>
    </div>
    <div class="fav-list" v-if="loggedOn">
      <h2>Favorited Breweries</h2>
      <favorite-breweries/>
    </div>
  </div>
</template>

<script>
import FavoriteBreweries from '@/components/FavoriteBreweries.vue'
export default {
    name: "anonymous-user-page",
    components: {
      FavoriteBreweries
    },
    data(){
    return{
      loggedOn : localStorage.getItem("user")!==null
    }
  }
}
</script>

<style>
#main{
  display: flex;

}
#text {
    display: flex;
    align-items: center;
    flex-direction: column;
    background-color: rgba(53, 53, 53, 0.8);
    color: white;
    border-radius: 10px;
    flex-basis: 70%;
    box-shadow: 6px 6px 3px black;
}

#text > h1 {
  font-size: 3rem;

}

.fav-list{
  display: flex;
  flex-basis:30%;
  flex-direction: column;
  margin-left:10px;
  text-align: center;
  overflow: auto;
}
.fav-list > h2 {
  display: flex;
  justify-content: center;
  align-items: center;
background-color: rgba(53, 53, 53, 0.8);
    color: white;
    border-radius: 10px;
    box-shadow: 6px 6px 3px black;
    font-size:2.5em;
    margin-right: 10px;
    height: 4rem;
    margin-left: 10px;
}

#text > h2 {
  font-size: 2rem;
}

#text > p {
    font-size: 1.5rem;
    padding-right:30px;
}



</style>
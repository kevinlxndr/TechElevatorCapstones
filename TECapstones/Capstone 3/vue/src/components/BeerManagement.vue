<template>
  <div id='beerManagement'>
    <div id="contents">
        <div class="inner-block">
            <add-beer-form></add-beer-form>
            <h1>{{brewery.name}}</h1>
            <h1>Select Beer</h1>
            <div id="beermanagelist" v-for="beer in getBeers" :key="beer.beerId" @click='chooseBeer(beer.beerId)'>
                <h1>{{beer.name}}</h1>
            </div>
        </div>
        
    </div>
    <side-details  :currentBeer='this.$store.state.beers.filter(x=>x.beerId === currentBeer)[0]' v-if='showSide'/>
  </div>
</template>

<script>
import SideDetails from '@/components/SideDetails.vue'
import AddBeerForm from '@/components/AddBeerForm.vue'

export default {
    data(){
        return{
    showSide: false,
    currentBeer : -1,
    brewery: this.$store.state.breweries.filter(x=> x.breweryId === this.$store.state.currentBrewery)[0]
    }
    },
    components: {
        SideDetails,
        AddBeerForm
    },
    methods:{
        chooseBeer(id){
            this.showSide = true
            this.currentBeer = id
            this.$store.commit('SET_EDITING_MODE',2);
        },
    },
    computed:{
        getBeers(){
        return this.$store.state.beers.filter(beer =>beer.breweryId === this.brewery.breweryId && beer.isActive === 1)
        }
    }
}
</script>

<style>

#beerManagement{
    display: flex;
    flex-direction: row;
    align-items:stretch;
    border-radius: 10px;
    flex-basis: 100%;
}
#contents{
    display: flex;
    flex-direction: row;
    background-color: rgb(53,53,53);
    border-radius: 10px;
    padding: 1rem;
    justify-content: center;
    margin-right: 20px;
    flex-basis: 60%;
}
#beermanagelist{
    display: flex;
    border: 2px solid black;
    border-radius: 10px;
    padding: 1rem;
    margin-bottom:10px;
     justify-content: center;

}

#beermanagelist:hover{
    background-color: rgb(176, 179, 182);
    box-shadow: 5px 5px 3px black;
}
#beerlist>h1{
    display: flex;
    text-align: center;
}
.inner-block{
    display: flex;
    text-align: center;
    overflow: auto;
}

</style>

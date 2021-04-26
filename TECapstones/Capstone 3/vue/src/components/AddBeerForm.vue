<template>
<div>
    <button id="add-beer" v-if="showForm === false" @click.prevent="showForm = true">
            Add A Beer
    </button>
    <form class="beerform" @submit.prevent v-if="showForm === true">
        <div class="form-element">
            <label for="name">Name</label>
            <input type="text" id="name" placeholder = "Name" v-model="newBeer.name">
        </div>
        <div class="form-element">
            <label for="type">Select Beer Type</label>
            <select name="beer-type" id="type" v-model.number="newBeer.beerTypeId">
                <option value="1">Ale</option>
                <option value="2">Lager</option>
                <option value="3">IPA</option>
                <option value="4">Stout</option>
                <option value="5">Pilsner</option>
                <option value="6">Porter</option>
                <option value="7">Wheat</option>
            </select>
        </div>
        <div class="form-element">
            <label for="abv">ABV</label>
            <input type="text" id="abv" placeholder="ABV" v-model.number="newBeer.abv">
        </div>
        <div class="form-element">
            <label for="ingredients">Ingredients</label>
            <input type="text" id="ingredients" placeholder="Ingredients" v-model="newBeer.ingredients">
        </div>
        <div class="form-element">  
            <label for="description">Description</label>
            <textarea name="description" id="description" cols="30" rows="10" placeholder="Describe Beer here" v-model="newBeer.description" required></textarea>
        </div>
        <div class="form-element">
            <input type="submit" value="Submit" @click ="addBeer"/>
            <input type="button" value="Cancel" @click.prevent="resetForm" />
        </div>
        
    </form>
</div>
</template>

<script>
import BeerService from '../services/BeerService.js'
export default {
    name:"add-beer",
    data(){
        return{
            showForm: false,
            newBeer:{
                beerTypeId: 1,
                breweryId: this.$store.state.currentBrewery,
                name: "",
                abv: "",
                description: "",
                ingredients: "",
                isActive: 1
            }
        };
    },
    methods:{
        resetForm() {
            this.showForm = false;
            this.newBeer = {
                beerTypeId: 1,
                breweryId: this.$store.state.currentBrewery,
                name: "",
                abv: "",
                description: "",
                ingredients: "",
                isActive: 1};
        },
        addBeer(){
            BeerService
            .addBeer(this.newBeer)
            .then(response => {
                if (response.status === 201) {
                    this.showForm = false;
                    this.resetForm()
                    BeerService.getBeers().then(response => {
                    this.$store.state.beers =  response.data;})
                    this.$store.state.beerPictures.push('FullBeer.png')
                }
            })
            .catch(error => {
                console.log(error);
            });
        }
    }
}
</script>

<style>

</style>
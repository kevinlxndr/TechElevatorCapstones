<template>
<div id="edit-beer">
    <button   v-if="showForm === false" @click.prevent="showForm = true">
            Edit Beer
    </button>
    <form class="beerform" @submit.prevent v-if="showForm === true">
        <div class="form-element">
            <label for="nameEntry">Name</label>
            <input type="text" id="nameEntry" placeholder = "Name" v-model="newBeer.name" required>
        </div>
        <div class="form-element">
            <label for="type">Select Beer Type</label>
            <select name="beer-type" id="type" v-model.number="newBeer.beerTypeId" required>
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
            <input type="text" id="abv" placeholder="ABV" v-model.number="newBeer.abv" required>
        </div>
        <div class="form-element">
            <label for="ingredients">Ingredients</label>
            <input type="text" id="ingredients" placeholder="Ingredients" v-model="newBeer.ingredients" required>
        </div>
        <div class="form-element">  
            <label for="description">Description</label>
            <textarea name="description" id="description" cols="30" rows="10" placeholder="Describe Beer here" v-model="newBeer.description" required></textarea>
        </div>
        <div class="form-element">
            <input type="submit" value="Submit" @click ="editBeer"/>
            <input type="button" value="Cancel" @click.prevent="resetForm" />
        </div>
        
    </form>
</div>
</template>

<script>
import BeerService from '../services/BeerService.js'
export default {
    name:"edit-beer",
    data(){
        return{
            showForm: false,
        };
    },
    computed:{
        newBeer(){return this.beer}
    },
    props: ['beer'],
    methods:{
        resetForm() {
            this.showForm = false;
        },
        editBeer(){
            BeerService
            .editBeer(this.newBeer)
            .then(response => {
                if (response.status === 201) {
                    this.showForm = false;
                    BeerService.getBeers().then(response => {
                        this.$store.state.beers =  response.data;
                        })
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
#edit-beer{
    display: flex;
    flex-basis: 100%;
    width:100%;
}
.beerform{
    display: flex;
    flex-direction: column;
    align-items:center;
    flex-basis:100%;
}

.form-element{
    display:flex;
    flex-direction: column;
}
textarea{
    resize:none;
}
</style>
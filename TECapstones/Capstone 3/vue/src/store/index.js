import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

/*
 * The authorization header is set for axios when you login but what happens when you come back or
 * the page is refreshed. When that happens you need to check for the token in local storage and if it
 * exists you should set the header so that it will be attached to each request
 */
const currentToken = localStorage.getItem('token')

if(currentToken != null) {
  axios.defaults.headers.common['Authorization'] = `Bearer ${currentToken}`;
}
else{
  localStorage.removeItem('user')
}

const currentUser = JSON.parse(localStorage.getItem('user'));

export default new Vuex.Store({
  state: {
    token: currentToken || '',
    user: currentUser || {},
    currentDisplay : 0,
    currentBrewery : {},
    currentBeer : {},
    editingMode:0,
    breweries:[],
    beers:[],
    beerReviews:[],
    breweryReviews:[],
    showReviewForm:false,
    showEditForm:false,
    showAddForm:false,
    showPictureForm:false,
    userFavorites: [],
    beerTypeIdList:["NA","Ale","Lager","IPA","Stout","Pilsner","Porter","Wheat"],
    beerPictures:["penn_pilsner.jpeg", "penn_dark.jpeg", "southern_tier.png","southern_tier.png","2XIPA.jpg","nu_juice_ipa.png","southern_tier.png", "penn_gold.png", "penn_kaiser_pils.png","barbarian_brewing.png", "bane_of_existence.jpg","fruit_rush.jpg","hitchhiker_mystic_delusion.jpg"]
  },
  mutations: {
    SET_AUTH_TOKEN(state, token) {
      state.token = token;
      localStorage.setItem('token', token);
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    },
    SET_USER(state, user) {
      state.user = user;
      localStorage.setItem('user',JSON.stringify(user));
    },
    LOGOUT(state) {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      state.token = '';
      state.user = {};
      axios.defaults.headers.common = {};
    },
    ADD_BREWERY_REVIEW(state, breweryReview) {
      state.breweryReviews.unshift(breweryReview);
    },
    ADD_BEER_REVIEW(state, beerReview) {
      state.beerReviews.unshift(beerReview);
    },
    SET_CURRENT_PAGE(state,id){
      state.currentDisplay = id
    },
    SET_EDITING_MODE(state,id){
      state.editingMode = id;
    },
    ADD_BEER(state, beer){
      state.beers.unshift(beer);
    },
    ADD_BREWERY(state, brewery){
      state.breweries.unshift(brewery);
    },
    MAKE_REVIEW_PRIVATE(state, reviewToPrivate) {
      reviewToPrivate.private = ! reviewToPrivate.private;
    },
    ADD_USER_FAVORITE(state, favorite){
      state.userFavorites.unshift(favorite);
    },
    DELETE_USER_FAVORITE(state, favorite){
      state.userFavorites = state.userFavorites.filter(x=>x!==favorite);
    }
  }
})

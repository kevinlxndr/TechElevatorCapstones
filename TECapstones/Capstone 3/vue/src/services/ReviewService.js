import axios from 'axios';

export default {

  getBeerReviews(){
      return axios.get('/beerreview')
  },

  addBeerReview(beerReview){
    return axios.post('/beerreview', beerReview)
  },

  getBreweryReviews(){
    return axios.get('/breweryreview')
},

addBreweryReview(breweryReview){
  return axios.post('/breweryreview', breweryReview)
}

}
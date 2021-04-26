import axios from 'axios';

export default{
    addFavorites(favBrewery){
        return axios.post('/favBrewery', favBrewery);
    },

    deleteFavorite(favBrewery){
        return axios.delete(`/favBrewery/${favBrewery.UserId}/${favBrewery.BreweryId}`);
    },
    getFavorites(userId){
        return axios.get(`/favBrewery/${userId}`);
    }
}
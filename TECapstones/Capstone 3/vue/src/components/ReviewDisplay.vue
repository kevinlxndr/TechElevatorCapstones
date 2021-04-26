<template>
  <div class="review" v-if="reviews.length>0">
    <div v-if='reviewType' id='beerReviewDisplay'>
      <div  id='reviewpart'  v-for="review in reviews" :key='review.beerReviewId'>
        <h3>{{review.title}} </h3>
        <div id="rating">
          <img src="@/images/FullBeer.png"
          v-bind:title="review.beerRating"
          id="ratingBeer" v-for=" i in review.beerRating" 
          v-bind:key="i"/>
          </div>
          <h3 class="review-review">{{ review.review }}</h3>
      </div>
    </div>
    <div v-else-if="!reviewType " id='breweryReviewDisplay'>
      <div id='reviewpart'  v-for="review in reviews" :key='review.breweryReviewId'>
        <h3>{{ review.title }}</h3>
        <div id="rating">
          <img src="@/images/FullBeer.png"
          v-bind:title="review.breweryRating"
          id="ratingBeer" v-for=" i in review.breweryRating" 
          v-bind:key="i"
          />
        </div>
        <h3 class="review-review">{{ review.review }}</h3>
      </div>
    </div>
  </div>
</template>

<script>
export default {
    name: "review-display",
    data(){
      return{
      }
    },
    computed:{
      reviews(){
        if(this.reviewType === true){
         return this.$store.state.beerReviews.filter(x => (x.beerId === this.reviewId && x.isPrivate===0))
        }
        else{
         return this.$store.state.breweryReviews.filter(x => (x.breweryId === this.reviewId&& x.isPrivate===0))
        }
      }
    },
    methods: {},
    props:['reviewType','reviewId']
}
</script>

<style>
.review {
    display: flex;
    flex-direction: column;
    justify-content: space-around;
    width:100%;
    margin-top: 15px;
}

#reviewpart{
  flex-direction: column;
  border: 2px solid black;
  margin-top: 6px;
  border-radius: 5px;
}

#rating { 
display:flex;
height:50px;
width:100%;
justify-content: center;
margin-left: 30px;
}

#ratingBeer {
height: 100%;
margin-right: 5px;
margin-left: 5px;
}

h3 {
  text-align: left;
}


</style>


<template>
  <div id="average-beer-rating">
    <div id="rating">
        <img src="@/images/FullBeer.png"
        id="ratingBeer" v-for="i in Math.round(averageRating)" 
        v-bind:key="i"
        />
    </div>
    <div id="rating-amount">{{ averageRating }} Average Rating</div> 
  </div>
</template>

<script>
export default {
    name: "average-rating",
    computed: {
        averageRating() {
            let filteredReviews = this.$store.state.beerReviews.filter(review => review.beerId === this.numberOfBeer)
            if(filteredReviews.length === 0) {
                return 0;
            }
            let sum = filteredReviews.reduce((currentSum, currentReview) => 
                currentSum + currentReview.beerRating, 0);

            return parseFloat((sum / filteredReviews.length).toFixed(1));
        },
    },
    props: ['numberOfBeer']
};
</script>

<style>
#average-beer-rating{
    width:100%;
}
</style>
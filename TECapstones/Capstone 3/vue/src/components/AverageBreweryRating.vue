<template>
  <div id="average-brewery-rating">
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
            let filteredReviews = this.$store.state.breweryReviews.filter(review => review.breweryId === this.numberOfBrewery)
            if(filteredReviews.length === 0) {
                return 0;
            }
            let sum = filteredReviews.reduce((currentSum, currentReview) =>
                currentSum + currentReview.breweryRating, 0);

            return parseFloat((sum / filteredReviews.length).toFixed(1));
        },
    },
    props: ["numberOfBrewery"]
};
</script>

<style>
#average-brewery-rating{

    width:100%;
}
#rating-amount{
    text-align: center;
}
#rating{
        margin-left: 0px;
}
</style>
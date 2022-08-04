<template>

  <div class="container-fluid">
    <div class="masonry-container ">
      <Keep v-for="k in keeps" :keep="k" :vaults="myVaults" :vKeeps="vKeeps" class='masonry-item' />
      <!-- class="col-md-3 col-6" -->

    </div>
  </div>


</template>

<script>
import { onMounted, watchEffect, computed } from "vue"
import { AppState } from "../AppState"
import { api } from "../services/AxiosService"
import { logger } from "../utils/Logger"
import { keepsService } from "../services/KeepsService"

export default {
  name: 'Home',

  setup() {

    watchEffect(async () => {
      try {
        await keepsService.GetKeeps();
      } catch (error) {
        logger.error(error)
      }
    })

    return {
      keeps: computed(() => AppState.keeps),
      myVaults: computed(() => AppState.MyVaults),
      vKeeps: computed(() => AppState.MyVaultKeeps)


    }
  }
}
/*

function resizeGridItem(item){
  grid = document.getElementsByClassName("grid")[0];
  rowHeight = parseInt(window.getComputedStyle(grid).getPropertyValue('grid-auto-rows'));
  rowGap = parseInt(window.getComputedStyle(grid).getPropertyValue('grid-row-gap'));
  rowSpan = Math.ceil((item.querySelector('.content').getBoundingClientRect().height+rowGap)/(rowHeight+rowGap));
    item.style.gridRowEnd = "span "+rowSpan;
}

function resizeAllGridItems(){
  allItems = document.getElementsByClassName("item");
  for(x=0;x<allItems.length;x++){
    resizeGridItem(allItems[x]);
  }
}

function resizeInstance(instance){
  item = instance.elements[0];
  resizeGridItem(item);
}

window.onload = resizeAllGridItems();
window.addEventListener("resize", resizeAllGridItems);

allItems = document.getElementsByClassName("item");
for(x=0;x<allItems.length;x++){
  imagesLoaded( allItems[x], resizeInstance);
}
*/



</script>

<style scoped lang="scss">
.masonry-container {
  columns: 6 200px;
  column-gap: 1rem;
}

.masonry-item {
  display: inline-block;
  width: 100%;
  text-align: center;
}

.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>

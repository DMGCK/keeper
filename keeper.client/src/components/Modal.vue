<template lang="">


  <!-- Button trigger modal -->
  <!-- <button type="button" class="btn btn-primary btn-lg" data-bs-toggle="modal" data-bs-target="#modelId">
    Launch
  </button> -->

  <!-- Modal -->
  <div class="modal fade" :id="`modal${id}`" tabindex="-1" role="dialog" aria-labelledby="modelTitleId" aria-hidden="true">
    <div class="modal-dialog modal-lg" role="document">
      <div class="modal-content">
        <div class="modal-body">
          <div class="move-it-container">
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
            <slot>
    This content is currently unavailable.
  </slot>
        </div>
      </div>
    </div>
  </div>


</template>
<script>
export default {
  props: {
    id: {
      type: Number,
      required: true
    }
  }

}
</script>
<style scoped lang="scss">
.move-it-container {
  display: flex;
  flex-direction: row-reverse;
}
</style>
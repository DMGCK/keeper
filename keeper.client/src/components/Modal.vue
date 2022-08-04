<template lang="">


  <!-- Button trigger modal -->
  <!-- <button type="button" class="btn btn-primary btn-lg" data-bs-toggle="modal" data-bs-target="#modelId">
    Launch
  </button> -->
<!-- :id="id == undefined || null || NaN ?`modal${id}` : `modal${formId}` " -->
  <!-- Modal -->
<!-- Button trigger modal -->
<!-- <button type="button" class="btn btn-primary btn-lg" data-bs-toggle="modal" data-bs-target="#modelId">
  Launch
</button> -->

<!-- Modal -->
<!-- :id="modalTarget" -->




</template>
<script>
export default {
  props: {
    modalTarget: {
      type: String,
      required: false
    },
    formId: {
      type: String,
      required: false
    }
  }

}
</script>
<style scoped lang="scss">
</style>
<template lang="">
  <div @click="profileLink" class="rounded selectable p-1 px-2">
    <div class="d-flex justify-content-apart">
      <div class="d-flex flex-column justify-content-center">
        {{ creator.name }} 
      </div>

      <img :src="creator.picture" class="profile-img" />
    </div>
  </div>
</template>
<script>
import { router } from "../router"

export default {
  props: {
    creator: {
      type: Object,
      Required: true
    }
  },

  setup(props) {

    return {
      profileLink() {
        router.push({
          name: "Profile",
          params: {
            id: props.creator.id
          }
        })
      }
    }
  }
}
</script>
<style scoped lang="scss">
.profile-img {
  width: 50px;
  height: 50px;
  border-radius: 5px;
}
</style>
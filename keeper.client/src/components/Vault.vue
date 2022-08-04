<template lang="">
  <div  class=" col-md-3 col-6 ">
              <div @click="navToVault" class="vault-card selectable">
                <img alt="vault image" :src="vault.img" class="rounded elevation-2 fit-this" />
                <div class="vault-text">
                  <div>{{vault.name}}</div>
                </div>
              </div>
            </div>
</template>
<script>
import { AppState } from "../AppState"
import { router } from "../router"

export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup(props) {

    return {
      navToVault() {

        if (props.vault.isPrivate == true && AppState.account.id != props.vault.creatorId) {
          router.push({
            name: "Home"
          });
        }
        router.push({
          name: "Vault",
          params: {
            id: props.vault.id
          }
        })
      }
    }
  }

}
</script>
<style scoped lang="scss">
.vault-card {
  width: 100%;
  height: 100%;
  max-height: 280px;
  position: relative;
  margin-top: 2rem;
}

.fit-this {
  object-fit: cover;
  width: 100%;
  height: 100%;
}

.vault-text {
  border-radius: 5px;
  padding: .5rem;
  position: absolute;
  background-color: rgba(245, 245, 245, 0.655);
  bottom: 10px;
  left: 10px;
  font-weight: bold;
}
</style>
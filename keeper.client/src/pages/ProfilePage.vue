<template lang="">
  <div>
     <div class="container">
    <div class="row">
      <div class="col-12">
        <div class="d-flex my-5">

          <img :src="account.picture" />
          <div class="display-4 fw-bold p-3">
            {{ account.name }}
          </div>

        </div>
        <div class="ms-5 ps-5 fs-2 fst-italic">
          <div>Vaults: {{ vaults.length }}</div>
          <div>Keeps: {{ keeps.length }}</div>
        </div>
      </div>
      <!-- Vaults -->
      <div class="col-12 my-5">
        <div>
          <div class="display-6 fw-bold text-decoration-underline">Vaults
            <span> Add Button </span>
          </div>
        </div>
        <div>
          <div class="row">
            <!-- Template - Componentize -->
            <Vault v-for="v in vaults" :vault="v" class="my-2 my-md-1" />
          </div>
        </div>

        <div class="row">
          <div class="col-12">

            <div class="display-6 fw-bold text-decoration-underline my-3">Keeps</div>
            <div class="masonry-container ">
              <Keep v-for="k in keeps" :keep="k" :vaults="vaults" :vKeeps="vKeeps" class='masonry-item' />
            </div>
          </div>
        </div>

      </div>
    </div>
  </div>
  </div>
</template>
<script>
import { computed, onMounted } from "vue"
import { useRoute } from "vue-router"
import { AppState } from "../AppState"
import { keepsService } from "../services/KeepsService"
import { vaultKeepsService } from "../services/VaultKeepsService"
import { vaultsService } from "../services/VaultsService"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { profilesService } from "../services/ProfilesService"
export default {
  name: "Profile",
  setup() {
    const route = useRoute();

    onMounted(async () => {
      try {
        await profilesService.GetActiveProfile(route.params.id)
        await keepsService.GetActiveUserKeeps(route.params.id)
        await vaultsService.GetActiveUserVaults(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error, "error")
      }
    })

    return {
      account: computed(() => AppState.profile),
      vaults: computed(() => AppState.ActiveVaults),
      vKeeps: computed(() => AppState.ActiveVaultKeeps),
      keeps: computed(() => AppState.ActiveUserKeeps)
    }
  }

}
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
</style>
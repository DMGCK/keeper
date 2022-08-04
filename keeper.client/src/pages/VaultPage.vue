<template lang="">
  <div>
    <div>


      <div class="d-flex justify-content-between m-3">
        <div>

          <div class="display-4 fw-bold">
            {{vault.name}}
          </div>
          <div class="display-6 fst-italic">
            Keeps: {{keeps.length}}
          </div>
        </div>
        <div class="d-flex flex-column justify-content-center">
          <button @click="deleteVault" v-show="isThisYourVault" class="btn btn-outline-danger">
            Delete Vault
          </button>
        </div>
      </div>


<!-- Masonry layout -->
      <div>
        <div class="masonry-container ">
          <Keep v-for="k in keeps" :keep="k" :vaults="myVaults" :vKeeps="vKeeps" :activeVault="vault" class='masonry-item' />
          <!-- class="col-md-3 col-6" -->

        </div>
      </div>



    </div>
  </div>
</template>
<script>
import { useRoute } from "vue-router"
import { computed, onMounted, popScopeId } from "vue";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { vaultsService } from "../services/VaultsService";
import { AppState } from "../AppState";
import { router } from "../router";

export default {
  name: "Vault",

  setup() {
    const route = useRoute();

    onMounted(async () => {
      try {
        vaultsService.GetActiveVault(route.params.id);
        vaultsService.GetCurrentVaultKeeps(route.params.id);
      } catch (error) {
        logger.error(error)
        Pop.toast(error, "error")
      }
    })

    return {
      keeps: computed(() => AppState.CurrentVaultKeeps),
      vault: computed(() => AppState.ActiveVault),
      isThisYourVault: computed(() => AppState.account.id == AppState.ActiveVault.creatorId),

      async deleteVault() {
        try {
          if (await Pop.confirm(`Are you sure you want to delete ${AppState.ActiveVault.name}`)) {
            await vaultsService.DeleteVault(route.params.id)
            router.push({
              name: "Account"
            })
          }
        } catch (error) {
          logger.log(error)
          Pop.toast(error, "error")
        }
      }
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
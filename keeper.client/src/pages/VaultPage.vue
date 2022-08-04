<template lang="">
  <div>

    <div class="container">
      <div class="row">
        <div class="col-12">
          <div class="d-flex justify-content-between m-3">
            <div>
              <div class="display-4 fw-bold">
               {{vault.name}}
              </div>
              <div class="display-6 fst-italic">
                Keeps: {{keeps.length}}
                <span v-if="vault.isPrivate">
                  - Private
                </span>
              </div>
            </div>
            <div class="d-flex flex-column justify-content-center">
              <button title="delete vault" @click="deleteVault" v-show="isThisYourVault" class="btn btn-outline-danger">
                Delete Vault
              </button>
            </div>
          </div>
        </div>
          <div class="col-12">
            <div>
              <div class="masonry-container ">
                <Keep v-for="k in keeps" :keep="k" :vaults="vaults"  :activeVault="vault" class='masonry-item' />
                <!-- :vKeeps="vKeeps" -->
              </div>
            </div>
           </div>
          </div>
        </div>
      </div>
</template>
<script>
import { useRoute, useRouter } from "vue-router"
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
    const router = useRouter();




    onMounted(async () => {
      try {
        await vaultsService.GetActiveVault(route.params.id);
        await vaultsService.GetCurrentVaultKeeps(route.params.id);
        console.log("MADE IT THROUGH THE TRY")

      } catch (error) {
        console.log("MADE IT THROUGH THE CATCH")

        if (AppState.ActiveVault.id == undefined) {
          console.log("I MADE IT INSIDE THE IF")

          router.push({
            name: "Home"
          })
        }
        logger.error(error)
        Pop.toast(error, "error")

      }
      // console.log("I MADE IT BEFORE THE IF")


      // AppState.account.id != AppState.ActiveVault.creatorId && AppState.account.id != undefined

    })

    return {
      vaults: computed(() => AppState.myVaults),
      keeps: computed(() => AppState.CurrentVaultKeeps),
      vault: computed(() => AppState.ActiveVault),
      isThisYourVault: computed(() => AppState.account.id == AppState.ActiveVault.creatorId && AppState.account.id != undefined),

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
      },
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
<template lang="">
  <div>
    <div class="rounded elevation-2 my-2">
      <div @click="viewDetails" data-bs-toggle="modal" :data-bs-target="`#modal${keep.id}`">
        <div class="ex-text-container">

          <img :src="keep.img" class="img-fluid selectable rounded" />

          <div class="ex-text">
            <div class="">
              <div class="d-flex justify-content-apart">
                <div class="d-flex flex-column justify-content-center">
                  {{ keep.name }} 
                </div>
                <div @click="profileLink" class="selectable">
                  <img :src="keep.creator.picture" class="profile-img" />

                </div>
              </div>

            </div>

          </div>
        </div>
      </div>

<!-- This is the format for the modal inside -->

      <Modal :id="keep.id"> 
        <div class="container">
          <div class="row">
            <div class="col-6">
              <img :src="keep.img" class="img-fluid rounded elevation-1 my-2" />
            </div>
            <div class="col-6">
              <div class="divider flex-grow-1">

                <!-- TOP -->
              <div class="">
                <div class="d-flex justify-content-around my-3  " >
                  <div>
                    Views: {{keep.views}}
                  </div>
                  <div>
                    Kept: {{keep.kept}}
                  </div>
                </div>
                <div class="display-6 my-2">
                  {{keep.name}}
                </div>
                <div class="my-2">
                  {{keep.description}}
                </div>
              </div>


              <!-- BOTTOM -->
              <div class="display-block">

                <div class="d-flex justify-content-between">
                  <div class="d-flex flex-column justify-content-center"> 
                    <div class="input-group mb-3">
                      <button  class="btn btn-outline-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">Dropdown</button>
                      <ul class="dropdown-menu">
                        <li @click="selectVault(v)" v-for="v in filteredVaults"><a class="dropdown-item">{{v.name}}</a></li>
                      </ul>
                    </div>
                  </div>
                <div v-if="account?.id == keep.creatorId" class="d-flex flex-column justify-content-center"> 
                  <button @click="deleteKeep" class="btn btn-danger" data-bs-toggle="modal" :data-bs-target="`#modal${keep.id}`">
                    Delete
                  </button>
                </div>

                <div @click="removeFromVault" v-if="account?.id == activeVault?.creatorId" class="d-flex flex-column justify-content-center"> 
                  <button class="btn btn-outline-danger" data-bs-toggle="modal" :data-bs-target="`#modal${keep.id}`">
                    Remove From Vault
                  </button>
                </div>

                <Creator :creator="keep.creator" data-bs-toggle="modal" :data-bs-target="`#modal${keep.id}`"/>
              </div>
            </div>
          </div>

            </div>

          </div>

        </div>
      </Modal>
    </div>
  </div>
</template>
<script>
import { applyStyles } from "@popperjs/core";
import { computed, onMounted } from "vue";
import { keepsService } from "../services/KeepsService";
import { AppState } from "../AppState"
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { vaultKeepsService } from "../services/VaultKeepsService"
import { router } from "../router";

export default {
  props: {
    keep: {
      type: Object,
      required: true
    },
    vaults: {
      type: Array,
      required: false
    },
    vKeeps: {
      type: Array,
      required: false
    },
    activeVault: {
      type: Object,
      required: false
    }
  },
  setup(props) {

    return {
      account: computed(() => AppState.account),
      activeKeep: computed(() => AppState.activeKeep),
      filteredVaults: computed(() => {
        return AppState.MyVaults


        //look thru list of vaults and disable vaults that share both ID's with any vKeeps item.
        //any of the vkeeps that match this keep
        // return vaults?.Filter(v => {
        //   const found = vKeeps.Filter(vK => vK.keepId != props.keep.id && v.id != vK.vaultId)
        //   if (found) {
        //     return false
        //   }
        //   return true

        // })
      }),

      async viewDetails() {
        props.keep.views += 1;
        // console.log(props.keep.views)
        try {
          await keepsService.GetKeepById(props.keep.id);
          console.log(AppState.ActiveKeep)
          const aKeep = AppState.ActiveKeep
          props.keep.views = aKeep.views
          props.keep.keeps = aKeep.views
        } catch (error) {
          logger.error(error)
          Pop.toast(error, 'error');
        }
      },
      async deleteKeep() {
        if (await Pop.confirm("Are you sure you'd like to delete this keep?")) {
          try {
            await keepsService.deleteKeep(props.keep.id)
            Pop.toast("Deleted Keep");
          } catch (error) {
            Pop.toast(error, 'error');
            logger.error(error)
          }
        }
      },
      async selectVault(vault) {
        console.log("adding to vault", vault.id)
        try {
          const vKeep = {
            vaultId: vault.id,
            keepId: props.keep.id
          }
          vaultKeepsService.AddToVault(vKeep)
          Pop.toast(`Added To ${vault.name}`);
          props.keep.kept += 1;
        } catch (error) {
          Pop.toast(error, 'error');
          logger.error(error)
        }
      },
      async removeFromVault() {
        console.log(props.keep)
        if (await Pop.confirm(`Are you sure you'd like to remove this keep from this vault?`)) {
          try {
            await vaultKeepsService.deleteVKeep(props.keep.vaultKeepId, props.keep.id)
            Pop.toast("Removed From Vault");
          } catch (error) {
            Pop.toast(error, 'error');
            logger.error(error)
          }
        }
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

.ex-text-container {
  position: relative;
  height: 100%;
  width: 100%;
}

.ex-text {
  border-radius: 5px;
  padding-left: 2rem;
  background-color: rgba(250, 235, 215, 0.433);
  position: absolute;
  // transform: translateX(140%) translateY(-110%);
  // top: 80%;
  bottom: 10px;
  right: 10px;
  z-index: 5;
}

.divider {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}
</style>
<template lang="">
  <div>
    <div class="rounded elevation-2 my-2">
      <div title="open keep" @click="viewDetails" data-bs-toggle="modal"  :data-bs-target="`#keep${keep.id}`">
        <div class="ex-text-container">

          <img alt="Keep Image" :src="keep.img" class="img-fluid selectable rounded" />

          <div class="ex-text">
            <div class="">
              <div class="d-flex justify-content-apart">
                <div class="d-flex flex-column justify-content-center">
                  <div class=" pe-4">
                    {{ keep.name }} 
                    </div>
                </div>
                <div title="open profile" @click="profileLink" class="selectable">
                  <img alt="Profile Image" :src="keep.creator?.picture" class="profile-img" />
                </div>
              </div>

            </div>

          </div>
        </div>
      </div>

<!-- This is the format for the modal inside -->

<div class="modal fade" :id="`keep${keep.id}`"  aria-labelledby="modelTitleId" aria-hidden="true">
  <div class="modal-dialog modal-xl" role="document">
    <div class="modal-content">
      <div class="modal-body">
        
        <div class="move-it-container">
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          
          
          <div>


<div class="container">
          <div class="row">
            <div class="col-6">
              <img alt="keep image" :src="keep.img" class="img-fluid rounded elevation-1 my-2" />
            </div>
            <div class="col-6 d-flex flex-column justify-content-between">
              <div class="row">

                <!-- TOP -->
              <div class="col-12">
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
              </div>

              <div class="row ">
              <!-- BOTTOM -->
              <div class="col-12 ">

                <div class="d-flex justify-content-between">
<!-- Drop Down Button -->
                  <div class="d-flex flex-column justify-content-center"> 
                    <div class="input-group">
                      <button  class="btn btn-outline-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">Add To Vault</button>
                      <ul class="dropdown-menu">
                        <li title="Select Vault" @click="selectVault(v)" v-for="v in filteredVaults"><a class="dropdown-item">{{v.name}}</a></li>
                      </ul>
                    </div>
                  </div>

<!-- DELETE IF IT"S YOURS -->


              <div class="d-flex flex-column justify-content-center"> 
                <div v-if="account?.id == keep.creatorId" class="d-flex flex-column justify-content-center"> 
                  <button title="Delete Keep" @click="deleteKeep" class="btn btn-danger" data-bs-toggle="modal" :data-bs-target="`#modal${keep.id}`">
                    Delete
                  </button>
                </div>
               </div>

            </div>
            
            <div class="col-12">
                <!-- REMOVE FROM VAULT BUTTON -->
              <div class="d-flex justify-content-center"> 
                <div>
                <div title="Remove from vault" @click="removeFromVault" v-if="account?.id == activeVault?.creatorId && activeVault?.id != undefined" class="d-flex flex-column justify-content-center"> 
                    <button class="btn btn-outline-danger" data-bs-toggle="modal" :data-bs-target="`#modal${keep.id}`">
                      Remove From Vault
                    </button>
                </div>
                </div>
              </div>
            </div>


                <!-- CREATOR OBJECT -->
                <div class="col-12 d-flex justify-content-center my-2">
                  <Creator :creator="keep.creator" data-bs-toggle="modal" :data-bs-target="`#modal${keep.id}`"/>

                </div>

              </div>
          </div>
            </div>
          </div>
        </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>
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
import { useRoute } from "vue-router";

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
    // vKeeps: {
    //   type: Array,
    //   required: false
    // },
    activeVault: {
      type: Object,
      required: false
    }
  },
  setup(props) {

    const route = useRoute();

    return {
      route,
      account: computed(() => AppState.account),
      activeKeep: computed(() => AppState.activeKeep),
      filteredVaults: computed(() => {
        return AppState.MyVaults
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
  font-weight: bold;
  border-radius: 5px;
  padding-left: 2rem;
  background-color: rgba(250, 235, 215, 0.74);
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

.move-it-container {
  display: flex;
  flex-direction: row-reverse;
}
</style>
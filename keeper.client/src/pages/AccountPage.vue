<template>
  <div class="container">
    <div class="row">
      <div class="col-12">
        <div class="d-flex my-5">

          <img alt="Account picture" :src="account.picture" />
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
        <div class="d-flex justify-content-between">
          <div class="display-6 fw-bold text-decoration-underline">Vaults</div>
          <div>
            <button class="btn btn-outline-secondary" data-bs-toggle="modal" data-bs-target="#vault">
              New Vault
            </button>
          </div>
        </div>
        <div>
          <div class="row">
            <!-- Template - Componentize -->
            <Vault v-for="v in vaults" :vault="v" class="my-2 my-md-1" />
          </div>
        </div>

        <div class="row my-5">
          <div class="col-12">

            <div class="d-flex justify-content-between">
              <div class="display-6 fw-bold text-decoration-underline">Keeps</div>
              <div>
                <button class="btn btn-outline-secondary" data-bs-toggle="modal" data-bs-target="#keep">
                  New Keep
                </button>
              </div>
            </div>
            <div class="masonry-container ">
              <Keep v-for="k in keeps" :keep="k" :vaults="vaults" class='masonry-item' />
              <!-- :vKeeps="vKeeps" -->
            </div>
          </div>
        </div>

      </div>
    </div>
  </div>

  <Modal :id="0" :formId="`keep`">

  </Modal>

  <Modal :id="0" :formId="`vault`">

  </Modal>

  <div class="modal fade" id="keep" tabindex="-1" role="dialog" aria-labelledby="modelTitleId" aria-hidden="true">
    <div class="modal-dialog modal-xl" role="document">
      <div class="modal-content">
        <div class="modal-body">
          <div class="">
            <span class="display-6 fst-italic">New Keep</span>

            <form @submit.prevent="createKeep">

              <div class="mb-3">
                <label for="title" class="form-label">Title</label>
                <input required type="text" class="form-control" name="title" id="title" aria-describedby="helpId"
                  placeholder="Title.." v-model="newKeep.name">
              </div>

              <div class="mb-3">
                <label for="imgUrl" class="form-label">Image Url</label>
                <input required type="text" class="form-control" name="imgUrl" id="imgUrl" aria-describedby="helpId"
                  placeholder="Image Url.." v-model="newKeep.img">
              </div>

              <div class="mb-3">
                <label for="description" class="form-label">Description</label>
                <textarea required class="form-control" name="description" id="description" rows="3"
                  v-model="newKeep.description"></textarea>
              </div>

              <div class="d-flex flex-row-reverse">
                <button class="btn btn-success btn-outline-secondary" title="Submit Form" type="submit"
                  data-bs-toggle="modal" data-bs-target="#keep">
                  Create
                </button>
              </div>

            </form>

          </div>
        </div>
      </div>
    </div>
  </div>

  <div class="modal fade" id="vault" tabindex="-1" role="dialog" aria-labelledby="modelTitleId" aria-hidden="true">
    <div class="modal-dialog modal-xl" role="document">
      <div class="modal-content">
        <div class="modal-body">
          <div class="">
            <span class="display-6 fst-italic">New Vault</span>

            <form @submit.prevent="createNewVault">

              <div class="mb-3">
                <label for="title" class="form-label">Title</label>
                <input required type="text" class="form-control" name="title" id="title" aria-describedby="helpId"
                  placeholder="Title.." v-model="newVault.name">
              </div>

              <div class="mb-3">
                <label for="imgUrl" class="form-label">Image Url</label>
                <input required type="text" class="form-control" name="imgUrl" id="imgUrl" aria-describedby="helpId"
                  placeholder="Image Url.." v-model="newVault.img">
              </div>

              <div class="form-check form-switch">
                <input v-model="newVault.isPrivate" class="form-check-input" type="checkbox" id="checkbox">
                <label class="form-check-label" for="flexSwitchCheckDefault">Private Vault?</label>
              </div>

              <div class="d-flex flex-row-reverse">
                <button class="btn btn-success btn-outline-secondary" title="submit form" type="submit"
                  data-bs-toggle="modal" data-bs-target="#vault">
                  Create
                </button>
              </div>

            </form>

          </div>
        </div>
      </div>
    </div>
  </div>







</template>

<script>
import { computed, onMounted, ref, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from "../services/KeepsService"
import Pop from "../utils/Pop"
import { logger } from "../utils/Logger"
import { vaultsService } from "../services/VaultsService"
export default {
  name: 'Account',
  setup() {

    const newKeep = ref({})
    const newVault = ref({})

    return {
      newKeep,
      newVault,
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.MyVaults),
      vKeeps: computed(() => AppState.MyVaultKeeps),
      keeps: computed(() => AppState.MyKeeps),

      async createKeep() {
        try {
          await keepsService.createKeep(newKeep.value)
          // console.log(newKeep.value)
        } catch (error) {
          logger.error(error)
          Pop.toast(error, "error")
        }
      },
      async createNewVault() {
        console.log(newVault.value.isPrivate)
        if (newVault.value.isPrivate === undefined) {
          newVault.value.isPrivate = false;
        }
        try {
          await vaultsService.createNewVault(newVault.value)
          // console.log(newKeep.value)
        } catch (error) {
          logger.error(error)
          Pop.toast(error, "error")
        }
      },
      checktest() {
        console.log(document.getElementById("checkbox").checked)
      }
    }
  }
}
</script>

<style scoped>
/* img {
  max-width: 100px;
} */


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

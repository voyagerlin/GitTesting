<template>
  <transition name="modal" @after-leave="afterLeave">
    <dialog
      ref="modal"
      class="modal-container"
      v-show="innerModelValue"
      @click="handleClickOutSide"
    >
      <div class="header">
        <slot name="header">this is header</slot>
      </div>

      <div class="body">
        <slot>
          <iframe src="https://localhost:7004" frameborder="0"></iframe>
        </slot>
      </div>

      <div class="footer">
        <slot>
          <button class="btn" @click="closeModal">CLOSE</button>
          <button class="btn" @click="send">Send</button>
        </slot>
      </div>
    </dialog>
  </transition>
</template>

<script>
import { ref, computed, onMounted, watch } from "vue";
import  signalrConn  from "../utils/signalr-connect";

export default {
  props: {
    modelValue: Boolean,
  },
  setup(props, ctx) {
    const modal = ref();
    const closed = ref();

    const innerModelValue = computed({
      get: () => props.modelValue,
      set: (value) => ctx.emit("update:model-value", value),
    });

    const send = () => {
      console.log("closed", signalrConn);
      signalrConn.invoke("SendNotify", "test", "adfds1234");
    };
    const closeModal = () => {
      innerModelValue.value = false;
      // conn.invoke('SendNotify', 'vue user','hi all')
      // conn.send('SendNotify', 'send user','hi all')
      //conn.send('SendNotify',)
      console.log("close modal,", signalrConn.state);
    };
    const displayModal = (show) => {
      if (!modal.value) return;
      if (show) {
        modal.value.showModal();
      } else {
        modal.value.close();
      }
    };

    onMounted(() => {
      if (innerModelValue.value) displayModal(true);
      
    });

    watch(innerModelValue, (isOpen) => {
      if (isOpen) displayModal(true);
    });

    const afterLeave = () => displayModal(false);

    const handleClickOutSide = ({ clientX: x, clientY: y }) => {
      if (!modal.value) return;

      const { left, right, top, bottom } = modal.value.getBoundingClientRect();
      if (x < left || x > right || y < top || y > bottom)
        innerModelValue.value = false;
    };

    return {
      modal,
      afterLeave,
      displayModal,
      innerModelValue,
      handleClickOutSide,
      closeModal,
      send,
    };
  },
};
</script>

<style scoped>
.modal-enter-from,
.modal-leave-to {
  opacity: 0;
}

.modal-enter-active {
  transition-timing-function: "ease-out";
}

.modal-leave-active {
  transition-timing-function: "ease-in";
}

.modal-enter-active,
.modal-leave-active {
  transition-duration: 200ms;
}

.modal-enter-from.modal-container,
.modal-leave-to.modal-container {
  transform: scale(0.9) translateY(-2rem);
}

.modal-container::backdrop {
  background-color: rgba(0, 0, 0, 0.5);
}

.modal-container {
  display: flex;
  flex-direction: column;
  gap: 16px;
  width: 100%;
  height: 400px;
  margin: auto;
  max-width: 500px;
  border-radius: 10px;
  padding: 20px;
  background-color: antiquewhite;
  border: none;
}

.header {
  font-weight: bold;
  text-transform: uppercase;
}

.footer {
  margin-top: auto;
  display: flex;
  justify-content: flex-end;
}

.btn {
  width: fit-content;
  height: 30px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 14px;
}
</style>

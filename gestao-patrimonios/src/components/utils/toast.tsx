import { Slide, toast, ToastOptions } from "react-toastify";
import Button from "../button/button";

const defaultOptions: ToastOptions = {
  position: "bottom-right",
  autoClose: 2000,
  closeOnClick: true,
  draggable: true,
  transition: Slide,
};

export const notificacao = (msg: string) => toast.success(msg, defaultOptions);
export const erro = (msg: string) => toast.error(msg, defaultOptions);

export const toastConfirmarExcluir = (aoConfirmar: () => void) => {
  toast(
    ({ closeToast }) => (
      <div>
        <p>Deseja realmente excluir?</p>
        <div className="row">
          <Button
            className="btn"
            onClick={() => {
              aoConfirmar();
              closeToast();
            }}
          >
            Sim
          </Button>

          <Button className="btn2" onClick={closeToast}>
            Cancelar
          </Button>
        </div>
      </div>
    ),
    {
      autoClose: false,
      closeOnClick: false,
      draggable: false,
      transition: Slide,
    },
  );
};

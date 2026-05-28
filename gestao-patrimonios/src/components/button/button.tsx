import { ComponentPropsWithoutRef, ReactNode } from "react";

type ButtonProps = ComponentPropsWithoutRef<"button"> & {
  children: ReactNode;
};

const Botoes = ({ className, children, onClick, ...props }: ButtonProps) => {
  const combinedClasses = `btn ${className || ""}`.trim();

  return (
    <button className={combinedClasses} {...props} onClick={onClick}>
      {children}
    </button>
  );
};

export default Botoes;

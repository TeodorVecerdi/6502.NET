namespace Emulator;

public enum InstructionKind {
    /*
     * Load/Store Operations
     */

    /* Load Accumulator  */ LDA, // N,Z
    /* Load X Register   */ LDX, // N,Z
    /* Load Y Register   */ LDY, // N,Z
    /* Store Accumulator */ STA,
    /* Store X Register  */ STX,
    /* Store Y Register  */ STY,

    /*
     * Register Transfer Operations
     */
    /* Transfer Accumulator to X */ TAX, // N,Z
    /* Transfer Accumulator to Y */ TAY, // N,Z
    /* Transfer X to Accumulator */ TXA, // N,Z
    /* Transfer Y to Accumulator */ TYA, // N,Z

    /*
     * Stack Operations
     */
    /* Transfer Stack Pointer to X */ TSX, // N,Z
    /* Transfer X to Stack Pointer */ TXS,
    /* Push Accumulator            */ PHA,
    /* Push Processor Status       */ PHP,
    /* Pull Accumulator            */ PLA, // N,Z
    /* Pull Processor Status       */ PLP, // All

    /*
     * Logical Operations
     */
    /* Logical AND  */ AND, // N,Z
    /* Exclusive OR */ EOR, // N,Z
    /* Logical OR   */ ORA, // N,Z
    /* Bit Test     */ BIT, // N,V,Z

    /*
     * Arithmetic Operations
     */
    /* Add with Carry      */ ADC, // N,V,Z,C
    /* Subtract with Carry */ SBC, // N,V,Z,C
    /* Compare Accumulator */ CMP, // N,Z,C
    /* Compare X Register  */ CPX, // N,Z,C
    /* Compare Y Register  */ CPY, // N,Z,C

    /*
     * Increment/Decrement Operations
     */
    /* Increment a memory location */ INC, // N,Z
    /* Increment X Register        */ INX, // N,Z
    /* Increment Y Register        */ INY, // N,Z
    /* Decrement a memory location */ DEC, // N,Z
    /* Decrement X Register        */ DEX, // N,Z
    /* Decrement Y Register        */ DEY, // N,Z

    /*
     * Shift Operations
     */
    /* Arithmetic Shift Left  */ ASL, // N,Z,C
    /* Logical Shift Right    */ LSR, // N,Z,C
    /* Rotate Left            */ ROL, // N,Z,C
    /* Rotate Right           */ ROR, // N,Z,C

    /*
     * Jump Operations
     */
    /* Jump to New Location   */ JMP,
    /* Jump to Subroutine     */ JSR,
    /* Return from Subroutine */ RTS,

    /*
     * Branch Operations
     */
    /* Branch on Carry Clear     */ BCC,
    /* Branch on Carry Set       */ BCS,
    /* Branch on Zero Set        */ BEQ,
    /* Branch on Negative Set    */ BMI,
    /* Branch on Zero Clear      */ BNE,
    /* Branch on Negative Clear  */ BPL,
    /* Branch on Overflow Clear  */ BVC,
    /* Branch on Overflow Set    */ BVS,

    /*
     * Status Flag Operations
     */
    /* Clear Carry Flag     */ CLC, // C
    /* Clear Decimal Mode   */ CLD, // D
    /* Clear Interrupt Flag */ CLI, // I
    /* Clear Overflow Flag  */ CLV, // V
    /* Set Carry Flag       */ SEC, // C
    /* Set Decimal Mode     */ SED, // D
    /* Set Interrupt Flag   */ SEI, // I

    /*
     * System Functions
     */
    /* Force Break           */ BRK, // B
    /* No Operation          */ NOP,
    /* Return from Interrupt */ RTI, // All
}
/**
 * Copyright (c) 2015-present, Facebook, Inc.
 * All rights reserved.
 *
 * This source code is licensed under the BSD-style license found in the
 * LICENSE file in the root directory of this source tree. An additional grant
 * of patent rights can be found in the PATENTS file in the same directory.
 *
 * @providesModule Modal
 * @flow
 */
'use strict';

const AppContainer = require('AppContainer');
const I18nManager = require('I18nManager');
const Platform = require('Platform');
const React = require('React');
const StyleSheet = require('StyleSheet');
const View = require('View');

const deprecatedPropType = require('deprecatedPropType');
const requireNativeComponent = require('requireNativeComponent');
const RCTModalHostView = requireNativeComponent('RCTModalHostView', null);

const PropTypes = React.PropTypes;

/**
 * The Modal component is a simple way to present content above an enclosing view.
 *
 * _Note: If you need more control over how to present modals over the rest of your app,
 * then consider using a top-level Navigator._
 *
 * ```javascript
 * import React, { Component } from 'react';
 * import { Modal, Text, TouchableHighlight, View } from 'react-native';
 *
 * class ModalExample extends Component {
 *
 *   state = {
 *     modalVisible: false,
 *   }
 *
 *   setModalVisible(visible) {
 *     this.setState({modalVisible: visible});
 *   }
 *
 *   render() {
 *     return (
 *       <View style={{marginTop: 22}}>
 *         <Modal
 *           animationType={"slide"}
 *           visible={this.state.modalVisible}
 *           >
 *          <View style={{backgroundColor: 'rgba(0,0,0,0.5)', flex: 1, justifyContent: 'center', alignItems: 'center'}}>
 *           <View style={{backgroundColor: '#ffffff', width: 200, height: 100, padding: 5, borderRadius: 10}}>
 *             <Text>Hello World!</Text>
 *
 *             <TouchableHighlight onPress={() => {
 *               this.setModalVisible(!this.state.modalVisible)
 *             }}>
 *               <Text>Hide Modal</Text>
 *             </TouchableHighlight>
 *
 *           </View>
 *          </View>
 *         </Modal>
 *
 *         <TouchableHighlight onPress={() => {
 *           this.setModalVisible(true)
 *         }}>
 *           <Text>Show Modal</Text>
 *         </TouchableHighlight>
 *
 *       </View>
 *     );
 *   }
 * }
 * ```
 */
class Modal extends React.Component {
  static propTypes = {
    /**
     * The `animationType` prop controls how the modal animates.
     *
     * - `slide` slides in from the bottom
     * - `fade` fades into view
     * - `none` appears without an animation
     */
    animationType: PropTypes.oneOf(['none', 'slide', 'fade']),
    transparent: deprecatedPropType(
      PropTypes.bool,
      'Use a wrapping `View` or `TouchableOpacity` with a `backgroundColor` instead.'
    ),
    /**
     * The `visible` prop determines whether your modal is visible.
     */
    visible: PropTypes.bool,
    animated: deprecatedPropType(
      PropTypes.bool,
      'Use the `animationType` prop instead.'
    ),
  };

  static defaultProps = {
    visible: true,
  };

  static contextTypes = {
    rootTag: React.PropTypes.number,
  };

  render(): ?React.Element<any> {
    if (this.props.visible === false) {
      return null;
    }

    let animationType = this.props.animationType;
    if (!animationType) {
      // manually setting default prop here to keep support for the deprecated 'animated' prop
      animationType = 'none';
      if (this.props.animated) {
        animationType = 'slide';
      }
    }

    const innerChildren = __DEV__ ?
      ( <AppContainer rootTag={this.context.rootTag}>
          {this.props.children}
        </AppContainer>) :
      this.props.children;

    return (
      <RCTModalHostView
        animationType={animationType}
        style={styles.modal}
        >
        {innerChildren}
      </RCTModalHostView>
    );
  }

  // We don't want any responder events bubbling out of the modal.
  _shouldSetResponder(): boolean {
    return true;
  }
}

const styles = StyleSheet.create({
  modal: {
    position: 'absolute',
    top: 0,
    left: 0,
    right: 0,
    bottom: 0,
  }
});

module.exports = Modal;
